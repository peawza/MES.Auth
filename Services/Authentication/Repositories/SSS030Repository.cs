using Application;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using Utils.Constants;

namespace Authentication.Repositories
{
    public interface ISSS030Repository
    {
        UserSearchResultDo SearchUser(UserSearchCriteriaDo oCriteria);
        UserDo? GetUser(UserCriteriaDo oCriteria);
        string ValidateUser(UpdateUserDo oUser);
    }

    public class SSS030Repository : ISSS030Repository, IDisposable
    {
        private SystemDbContext db { get; set; }
        private ApplicationDbContext appDb { get; set; }

        public SSS030Repository(SystemDbContext db, ApplicationDbContext appDb)
        {
            this.db = db;
            this.appDb = appDb;
        }

        public UserSearchResultDo SearchUser(UserSearchCriteriaDo oCriteria)
        {
            try
            {
                UserSearchResultDo result = new UserSearchResultDo();

                var dbResult = (from uu in (from u in this.appDb.Users
                                            join ui in this.appDb.UserInfos.AsNoTracking()
                                                on u.Id equals ui.Id
                                            where (oCriteria.UserName == null
                                                    || u.UserName == oCriteria.UserName)
                                                    && (oCriteria.Name == null
                                                        || (ui.FirstName + " " + ui.LastName).Contains(oCriteria.Name))
                                                    && (oCriteria.ActiveFlag == null
                                                        || (oCriteria.ActiveFlag == u.ActiveFlag))
                                            select new
                                            {
                                                Id = u.Id,
                                                UserName = u.UserName,
                                                FirstName = ui.FirstName,
                                                LastName = ui.LastName,
                                                Email = u.Email,
                                                UpdateDate = ui.UpdateDate,
                                                ActiveFlag = u.ActiveFlag
                                            }).AsEnumerable()
                                join _sur in (from ur in (from u in this.appDb.UserRoles.AsNoTracking()
                                                          join r in this.appDb.ApplicationRoles.AsNoTracking()
                                                              on new { u.RoleId } equals new { RoleId = r.Id }
                                                          where r.AppCode == oCriteria.AppCode
                                                          select new
                                                          {
                                                              UserId = u.UserId,
                                                              RoleId = u.RoleId
                                                          }).AsEnumerable()
                                              join _fr in oCriteria.RoleIds
                                                  on new { ur.RoleId } equals new { RoleId = _fr } into g_fr
                                              from fr in g_fr.DefaultIfEmpty()

                                              where (oCriteria.RoleIds.Count == 0
                                                      || fr != null)
                                              group ur by ur.UserId into g
                                              select new
                                              {
                                                  UserId = g.Key
                                              }).ToList()
                                    on uu.Id equals _sur.UserId into g_sur
                                from sur in g_sur.DefaultIfEmpty()

                                where (oCriteria.RoleIds.Count == 0
                                        || sur != null)

                                orderby uu.UserName

                                select new UserSearchDo()
                                {
                                    Id = uu.Id,
                                    UserName = uu.UserName,
                                    Name = uu.FirstName + " " + uu.LastName,
                                    Email = uu.Email,
                                    UpdateDate = uu.UpdateDate,
                                    ActiveFlag = uu.ActiveFlag
                                });

                //result.TotalRecords = dbResult.Count();

                //dbResult = (from r in dbResult
                //            orderby r.UserName
                //            select r)
                //    .Skip(oCriteria.Skip)
                //    .Take(oCriteria.Take);

                var dbResultList = dbResult.ToList();

                List<UserRoleSearchDo> roles = (from uur in (from ur in this.appDb.UserRoles.AsNoTracking()
                                                             join r in this.appDb.ApplicationRoles.AsNoTracking()
                                                                 on ur.RoleId equals r.Id
                                                             where r.ActiveFlag == true
                                                                     && r.AppCode == oCriteria.AppCode
                                                             select new
                                                             {
                                                                 UserId = ur.UserId,
                                                                 RoleId = r.Id,
                                                                 RoleName = r.RoleName
                                                             }).AsEnumerable()
                                                join u in dbResultList
                                                    on uur.UserId equals u.Id

                                                orderby uur.UserId, uur.RoleName

                                                select new UserRoleSearchDo()
                                                {
                                                    UserId = uur.UserId,
                                                    RoleId = uur.RoleId,
                                                    RoleName = uur.RoleName
                                                }).ToList();

                List<UserSearchDo> users = (from u in dbResultList
                                            select new UserSearchDo()
                                            {
                                                Id = u.Id,
                                                UserName = u.UserName,
                                                Name = u.Name,
                                                Email = u.Email,
                                                UpdateDate = u.UpdateDate,
                                                ActiveFlag = u.ActiveFlag
                                            }).ToList();

                foreach (UserSearchDo user in users)
                {
                    user.Roles = roles.Where(x => x.UserId == user.Id).ToList();
                }

                result.Rows = users;
                result.TotalRecords = result.Rows.Count;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserDo? GetUser(UserCriteriaDo oCriteria)
        {
            try
            {
                UserDo? user = null;
                if (oCriteria.UserId != null)
                {
                    user = (from u in this.appDb.Users
                            join ui in this.appDb.UserInfos.AsNoTracking()
                                on u.Id equals ui.Id
                            join uiu in this.appDb.UserInfos.AsNoTracking()
                                on new { ui.UserNumber } equals new { uiu.UserNumber }
                            where u.Id == oCriteria.UserId
                            select new UserDo()
                            {
                                Id = u.Id,
                                UserName = u.UserName,
                                ActiveFlag = u.ActiveFlag,
                                FirstName = ui.FirstName,
                                LastName = ui.LastName,
                                Email = u.Email,
                                Remark = ui.Remark,
                                UpdateDate = ui.UpdateDate,
                                UpdateByName = (uiu != null ? uiu.FirstName + " " + uiu.LastName : ""),
                                LastLoginDate = u.LastLoginDate,
                                ActiveDate = u.ActiveDate,
                                InActiveDate = u.InActiveDate
                            }).FirstOrDefault();
                }
                if (user == null)
                {
                    user = new UserDo()
                    {
                        ActiveFlag = true
                    };
                }
                if (user.Id != null)
                {
                    user.Roles = (from ur in this.appDb.UserRoles.AsNoTracking()
                                  join r in this.appDb.ApplicationRoles.AsNoTracking()
                                      on ur.RoleId equals r.Id
                                  where r.ActiveFlag == true
                                          && ur.UserId == user.Id
                                          && r.AppCode == oCriteria.AppCode

                                  orderby r.Name

                                  select new UserRoleDo()
                                  {
                                      RoleId = r.Id,
                                      Name = r.Name,
                                      RoleName = r.RoleName
                                  }).ToList();
                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ValidateUser(UpdateUserDo oUser)
        {
            try
            {
                if (oUser.Id != null)
                {
                    var user = (from ui in this.appDb.UserInfos.AsNoTracking().AsEnumerable()
                                where ui.Id == oUser.Id
                                        && (double.Parse(ui.UpdateDate.ToString("yyyyMMddHHmmss")) >
                                                double.Parse(oUser.UpdateDate.Value.ToString("yyyyMMddHHmmss")))
                                select ui).FirstOrDefault();
                    if (user != null)
                    {
                        return string.Join(";", "E0007", oUser.UserName);
                    }
                }

                if ((from ui in this.appDb.UserInfos.AsNoTracking()
                     where (oUser.Id == null
                            || ui.Id != oUser.Id)
                            && ui.UserName == oUser.UserName
                     select ui).ToList().Count > 0)
                    return string.Join(";", "E0013", oUser.UserName);
                else if ((from u in this.appDb.Users
                          where (oUser.Id == null
                                 || u.Id != oUser.Id)
                                 && u.Email == oUser.Email
                          select u).ToList().Count > 0)
                    return string.Join(";", "E0013", oUser.Email);

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
                if (appDb != null)
                {
                    appDb.Dispose();
                    appDb = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
