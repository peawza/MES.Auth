using Application;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories
{
    public interface ISSS040Repository
    {
        RoleSearchResultDo SearchRole(RoleSearchCriteriaDo oCriteria);
        RoleDo GetRole(RoleCriteriaDo oCriteria);
        List<RoleScreenDo> GetScreens(RoleCriteriaDo oCriteria);
        List<RoleScreenDo> GetScreensAllowList(RoleCriteriaDo oCriteria);
        string ValidateRole(RoleDo oRole);
        string ValidateRoleForDelete(RoleDo oRole);
        UserInRoleSearchResultDo SearchUserInRole(UserInRoleSearchCriteriaDo oCriteria);
    }

    public class SSS040Repository : ISSS040Repository, IDisposable
    {
        private SystemDbContext db { get; set; }
        private ApplicationDbContext appDb { get; set; }

        public SSS040Repository(SystemDbContext db, ApplicationDbContext appDb)
        {
            this.db = db;
            this.appDb = appDb;
        }

        public RoleSearchResultDo SearchRole(RoleSearchCriteriaDo oCriteria)
        {
            try
            {
                RoleSearchResultDo result = new RoleSearchResultDo();

                var users = (from ur in this.appDb.UserRoles.AsNoTracking()
                             join u in this.appDb.Users.AsNoTracking()
                                on ur.UserId equals u.Id
                             where (oCriteria.UserName == null
                                    || u.UserName == oCriteria.UserName)
                             group ur by ur.RoleId into g
                             select new
                             {
                                 RoleId = g.Key,
                                 NoUser = g.Count()
                             }).ToList();

                var dbResult = (from r in this.appDb.ApplicationRoles.AsNoTracking().AsEnumerable()

                                join sur in users
                                    on r.Id equals sur.RoleId into g
                                from urr in g.DefaultIfEmpty()

                                where r.AppCode == oCriteria.AppCode
                                        && (oCriteria.RoleName == null
                                        || r.RoleName.Contains(oCriteria.RoleName))
                                        && (oCriteria.Description == null
                                        || (r.Description != null
                                            && r.Description.Contains(oCriteria.Description)))
                                        && (oCriteria.UserName == null
                                            || urr != null)

                                select new RoleSearchDo()
                                {
                                    RoleId = r.Id,
                                    RoleName = r.RoleName,
                                    Description = r.Description,
                                    NoUser = urr != null ? urr.NoUser : 0,
                                    UpdateDate = r.UpdateDate,
                                    ActiveFlag = r.ActiveFlag
                                });

                //result.TotalRecords = dbResult.Count();

                //var x = dbResult.ToList();

                //result.Rows = (from r in dbResult
                //               orderby r.RoleName
                //               select r)
                //                .Skip(oCriteria.Skip)
                //                .Take(oCriteria.Take)
                //                .ToList();

                result.Rows = dbResult.ToList();
                result.TotalRecords = result.Rows.Count;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RoleDo GetRole(RoleCriteriaDo oCriteria)
        {
            try
            {
                RoleDo? role = (from r in this.appDb.ApplicationRoles.AsNoTracking()
                                join uic in this.appDb.UserInfos.AsNoTracking()
                                 on new { UserNumber = r.CreateBy } equals new { uic.UserNumber }
                                join uiu in this.appDb.UserInfos.AsNoTracking()
                                 on new { UserNumber = r.UpdateBy } equals new { uiu.UserNumber }
                                where r.Id == oCriteria.RoleId
                                select new RoleDo
                                {
                                    RoleId = r.Id,
                                    Name = r.Name,
                                    RoleName = r.RoleName,
                                    Description = r.Description,
                                    ActiveFlag = r.ActiveFlag,
                                    CreateDate = r.CreateDate,
                                    CreateByName = uic.FirstName + " " + uic.LastName,
                                    UpdateDate = r.UpdateDate,
                                    UpdateByName = uiu.FirstName + " " + uiu.LastName
                                }).FirstOrDefault();

                return role;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RoleScreenDo> GetScreens(RoleCriteriaDo oCriteria)
        {
            try
            {
                List<RoleScreenDo> screens = (from s in this.db.Screens.AsNoTracking()
                                              join sn in this.db.ScreenNames.AsNoTracking()
                                                  on s.ScreenId equals sn.ScreenId
                                              where s.ActiveFlag == true
                                                    && sn.Language == oCriteria.Language
                                              select new RoleScreenDo()
                                              {
                                                  ScreenId = s.ScreenId,
                                                  ImageIcon = s.ImageIcon,
                                                  ScreenName = s.ScreenId + ": " + sn.Name
                                              }).ToList();

                List<RoleScreenPermissionDo> permissions = (from sp in this.db.ScreenPermissions.AsNoTracking()

                                                            join s in this.db.Screens.AsNoTracking()
                                                                on new
                                                                {
                                                                    sp.AppCode,
                                                                    sp.ScreenId
                                                                } equals new
                                                                {
                                                                    s.AppCode,
                                                                    s.ScreenId
                                                                }

                                                            join p in this.db.Permissions.AsNoTracking()
                                                                on sp.PermissionCode equals p.PermissionCode

                                                            join pn in this.db.PermissionNames.AsNoTracking()
                                                                on new
                                                                {
                                                                    PermissionCode = p.PermissionCode,
                                                                    Language = oCriteria.Language
                                                                } equals new
                                                                {
                                                                    pn.PermissionCode,
                                                                    pn.Language
                                                                }

                                                            orderby sp.ScreenId, p.SeqNo

                                                            where sp.AppCode == oCriteria.AppCode
                                                                    && p.ActiveFlag == true

                                                            select new RoleScreenPermissionDo()
                                                            {
                                                                AppCode = sp.AppCode,
                                                                ScreenId = sp.ScreenId,
                                                                PermissionCode = sp.PermissionCode,
                                                                PermissionName = pn.Name,
                                                                HasPermission = false
                                                            }).ToList();
                permissions = (from p in permissions
                               join _rc in this.appDb.RoleClaims.AsNoTracking().AsEnumerable()
                                    on new
                                    {
                                        RoleId = oCriteria.RoleId,
                                        ClaimType = Constants.ROLE.CLAIM_TYPE_PERMISSION,
                                        ClaimValue = p.AppCode + "." + p.ScreenId + "." + p.PermissionCode
                                    } equals new
                                    {
                                        _rc.RoleId,
                                        _rc.ClaimType,
                                        _rc.ClaimValue
                                    } into g_rc
                               from rc in g_rc.DefaultIfEmpty()
                               select new RoleScreenPermissionDo()
                               {
                                   AppCode = p.AppCode,
                                   ScreenId = p.ScreenId,
                                   PermissionCode = p.PermissionCode,
                                   PermissionName = p.PermissionName,
                                   HasPermission = (rc != null)
                               }).ToList();

                foreach (RoleScreenDo screen in screens)
                {
                    screen.Permissions = permissions
                                            .Where(x => x.ScreenId == screen.ScreenId)
                                            .ToList();
                }

                return screens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidateRole(RoleDo oRole)
        {
            try
            {
                if (oRole.RoleId != null)
                {
                    var role = (from r in this.appDb.ApplicationRoles.AsNoTracking().AsEnumerable()
                                where r.Id == oRole.RoleId
                                        && (double.Parse(r.UpdateDate.ToString("yyyyMMddHHmmss")) >
                                                double.Parse(oRole.UpdateDate.Value.ToString("yyyyMMddHHmmss")))
                                select r).FirstOrDefault();
                    if (role != null)
                    {
                        return string.Join(";", "E0007", oRole.RoleName);
                    }
                }
                if ((from r in this.appDb.ApplicationRoles.AsNoTracking()
                     where (oRole.RoleId == null
                            || r.Id != oRole.RoleId)
                            && r.Name == oRole.RoleName
                     select r).ToList().Count > 0)
                    return string.Join(";", "E0013", oRole.RoleName);

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ValidateRoleForDelete(RoleDo oRole)
        {
            try
            {
                if (oRole.RoleId != null)
                {
                    if ((from ur in this.appDb.UserRoles.AsNoTracking()
                         where ur.RoleId == oRole.RoleId
                         select ur).Count() > 0)
                    {
                        return "E0055";
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserInRoleSearchResultDo SearchUserInRole(UserInRoleSearchCriteriaDo oCriteria)
        {
            try
            {
                UserInRoleSearchResultDo result = new UserInRoleSearchResultDo();

                var dbResult = (from ur in this.appDb.UserRoles.AsNoTracking()
                                join ui in this.appDb.UserInfos.AsNoTracking()
                                    on ur.UserId equals ui.Id
                                join u in this.appDb.Users
                                    on ur.UserId equals u.Id
                                where ur.RoleId == oCriteria.RoleId
                                        && (oCriteria.UserName == null
                                            || (ui.FirstName + " " + ui.LastName).Contains(oCriteria.UserName))
                                        && u.ActiveFlag == true
                                orderby ui.UserName
                                select new UserInRoleSearchDo()
                                {
                                    UserName = ui.UserName,
                                    Name = ui.FirstName + " " + ui.LastName
                                });

                //result.TotalRecords = dbResult.Count();

                //result.Rows = (from r in dbResult
                //               orderby r.UserName
                //               select r)
                //                .Skip(oCriteria.Skip)
                //                .Take(oCriteria.Take)
                //                .ToList();

                result.Rows = dbResult.ToList();
                result.TotalRecords = result.Rows.Count;

                return result;
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


        public List<RoleScreenDo> GetScreensAllowList(RoleCriteriaDo oCriteria)
        {
            string[] igoreScreenList = new string[6];
            igoreScreenList[0] = "SSS050";
            igoreScreenList[1] = "SSS051";
            igoreScreenList[2] = "SSS060";
            igoreScreenList[3] = "SSS061";
            igoreScreenList[4] = "SSS070";
            igoreScreenList[5] = "SSS071";

            try
            {
                List<RoleScreenDo> screens = (from s in this.db.Screens.AsNoTracking()
                                              join sn in this.db.ScreenNames.AsNoTracking()
                                                  on s.ScreenId equals sn.ScreenId
                                              where s.ActiveFlag == true
                                                    && sn.Language == oCriteria.Language
                                              select new RoleScreenDo()
                                              {
                                                  ScreenId = s.ScreenId,
                                                  ImageIcon = s.ImageIcon,
                                                  ScreenName = s.ScreenId + ": " + sn.Name
                                              }).ToList();

                screens = screens.Where(s => !igoreScreenList.Any(exclude => s.ScreenId.Contains(exclude))).ToList();

                List<RoleScreenPermissionDo> permissions = (from sp in this.db.ScreenPermissions.AsNoTracking()

                                                            join s in this.db.Screens.AsNoTracking()
                                                                on new
                                                                {
                                                                    sp.AppCode,
                                                                    sp.ScreenId
                                                                } equals new
                                                                {
                                                                    s.AppCode,
                                                                    s.ScreenId
                                                                }

                                                            join p in this.db.Permissions.AsNoTracking()
                                                                on sp.PermissionCode equals p.PermissionCode

                                                            join pn in this.db.PermissionNames.AsNoTracking()
                                                                on new
                                                                {
                                                                    PermissionCode = p.PermissionCode,
                                                                    Language = oCriteria.Language
                                                                } equals new
                                                                {
                                                                    pn.PermissionCode,
                                                                    pn.Language
                                                                }

                                                            orderby sp.ScreenId, p.SeqNo

                                                            where sp.AppCode == oCriteria.AppCode
                                                                    && p.ActiveFlag == true

                                                            select new RoleScreenPermissionDo()
                                                            {
                                                                AppCode = sp.AppCode,
                                                                ScreenId = sp.ScreenId,
                                                                PermissionCode = sp.PermissionCode,
                                                                PermissionName = pn.Name,
                                                                HasPermission = false
                                                            }).ToList();
                permissions = (from p in permissions
                               join _rc in this.appDb.RoleClaims.AsNoTracking().AsEnumerable()
                                    on new
                                    {
                                        RoleId = oCriteria.RoleId,
                                        ClaimType = Constants.ROLE.CLAIM_TYPE_PERMISSION,
                                        ClaimValue = p.AppCode + "." + p.ScreenId + "." + p.PermissionCode
                                    } equals new
                                    {
                                        _rc.RoleId,
                                        _rc.ClaimType,
                                        _rc.ClaimValue
                                    } into g_rc
                               from rc in g_rc.DefaultIfEmpty()
                               select new RoleScreenPermissionDo()
                               {
                                   AppCode = p.AppCode,
                                   ScreenId = p.ScreenId,
                                   PermissionCode = p.PermissionCode,
                                   PermissionName = p.PermissionName,
                                   HasPermission = (rc != null)
                               }).ToList();

                foreach (RoleScreenDo screen in screens)
                {
                    screen.Permissions = permissions
                                            .Where(x => x.ScreenId == screen.ScreenId)
                                            .ToList();
                }

                return screens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


}
