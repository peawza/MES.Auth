using Application;
using Application.Models;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories
{
    public interface ISSS050Repository
    {
        PermissionSearchResultDo SearchPermission(PermissionSearchCriteriaDo oCriteria);
        PermissionDo? GetPermission(PermissionDo oCriteria);
        PermissionUpdateResultDo AddPermission(PermissionUpdateDo oPermission);
        PermissionUpdateResultDo UpdatePermission(PermissionUpdateDo oPermission);
        PermissionUpdateResultDo DeletePermission(PermissionUpdateDo oPermission);

        void UpdatePermissionSeq(PermissionSeqUpdateDo oPermission);
    }

    public class SSS050Repository : ISSS050Repository, IDisposable
    {
        private SystemDbContext db { get; set; }

        public SSS050Repository(SystemDbContext db)
        {
            this.db = db;
        }

        public PermissionSearchResultDo SearchPermission(PermissionSearchCriteriaDo oCriteria)
        {
            try
            {
                PermissionSearchResultDo result = new PermissionSearchResultDo();

                var permissions = (from p in this.db.Permissions.AsNoTracking()
                                   join pn in this.db.PermissionNames.AsNoTracking()
                                       on new { p.PermissionCode, oCriteria.Language }
                                           equals new { pn.PermissionCode, pn.Language }

                                   join _spp in (from sp in this.db.ScreenPermissions.AsNoTracking()
                                                 select sp.PermissionCode).Distinct()
                                    on p.PermissionCode equals _spp into g_spp
                                   from spp in g_spp.DefaultIfEmpty()

                                   orderby p.SeqNo

                                   select new PermissionSearchDo()
                                   {
                                       PermissionCode = p.PermissionCode,
                                       PermissionName = pn.Name,
                                       Description = p.Description,
                                       SeqNo = p.SeqNo,
                                       ActiveFlag = p.ActiveFlag,
                                       PermissionUsed = spp != null
                                   }).ToList();

                result.Rows = permissions;
                result.TotalRecords = permissions.Count;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PermissionDo? GetPermission(PermissionDo oCriteria)
        {
            try
            {
                var permission = (from p in this.db.Permissions.AsNoTracking()

                                  where p.PermissionCode == oCriteria.PermissionCode

                                  select new PermissionDo()
                                  {
                                      PermissionCode = p.PermissionCode,
                                      Description = p.Description,
                                      ActiveFlag = p.ActiveFlag,
                                      UpdateDate = p.UpdateDate
                                  }).FirstOrDefault();
                if (permission != null)
                {
                    permission.PermissionNames = (from pn in this.db.PermissionNames.AsNoTracking()

                                                  where pn.PermissionCode == oCriteria.PermissionCode

                                                  select new PermissionNameDo()
                                                  {
                                                      PermissionCode = pn.PermissionCode,
                                                      Language = pn.Language,
                                                      Name = pn.Name
                                                  }).ToList();
                }

                return permission;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PermissionUpdateResultDo AddPermission(PermissionUpdateDo oPermission)
        {
            PermissionUpdateResultDo result = new PermissionUpdateResultDo();

            if ((from p in this.db.Permissions.AsNoTracking()
                 where p.PermissionCode == oPermission.PermissionCode
                 select p).FirstOrDefault() != null)
            {
                result.AddError(string.Format("E0013;{0}", oPermission.PermissionCode));
            }
            if (result.HasError == false)
            {
                try
                {
                    List<tb_PermissionName> names = new List<tb_PermissionName>();
                    foreach (var pName in oPermission.PermissionNames)
                    {
                        names.Add(new tb_PermissionName()
                        {
                            PermissionCode = oPermission.PermissionCode,
                            Language = pName.Language,
                            Name = pName.Name
                        });
                    }

                    int maxSeqNo = (from p in this.db.Permissions.AsNoTracking()
                                    select p.SeqNo).DefaultIfEmpty().Max();

                    this.db.Permissions.Add(new tb_Permission()
                    {
                        PermissionCode = oPermission.PermissionCode,
                        Description = oPermission.Description,
                        SeqNo = maxSeqNo + 1,
                        ActiveFlag = oPermission.ActiveFlag,
                        PermissionNames = names,
                        CreateDate = oPermission.CreateDate.Value,
                        CreateBy = oPermission.CreateBy.Value,
                        UpdateDate = oPermission.CreateDate.Value,
                        UpdateBy = oPermission.CreateBy.Value,
                    });

                    this.db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }
        public PermissionUpdateResultDo UpdatePermission(PermissionUpdateDo oPermission)
        {
            PermissionUpdateResultDo result = new PermissionUpdateResultDo();

            var permission = (from p in this.db.Permissions
                              where p.PermissionCode == oPermission.PermissionCode
                              select p).FirstOrDefault();
            if (permission == null)
                result.AddError("E0075");
            else if (permission.UpdateDate > oPermission.LatestUpdateDate)
                result.AddError("E0006");

            if (result.HasError == false)
            {
                try
                {
                    permission.Description = oPermission.Description;
                    permission.ActiveFlag = oPermission.ActiveFlag;
                    permission.UpdateDate = oPermission.UpdateDate.Value;
                    permission.UpdateBy = oPermission.UpdateBy.Value;

                    var permissionNames = (from pn in this.db.PermissionNames
                                           where pn.PermissionCode == oPermission.PermissionCode
                                           select pn).ToList();
                    foreach (var pName in oPermission.PermissionNames)
                    {
                        var pn = permissionNames
                            .Where(x => x.Language == pName.Language)
                            .FirstOrDefault();
                        if (pn != null)
                            pn.Name = pName.Name;
                        else
                        {
                            this.db.PermissionNames.Add(new tb_PermissionName()
                            {
                                PermissionCode = oPermission.PermissionCode,
                                Language = pName.Language,
                                Name = pName.Name
                            });
                        }
                    }

                    this.db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }
        public PermissionUpdateResultDo DeletePermission(PermissionUpdateDo oPermission)
        {
            PermissionUpdateResultDo result = new PermissionUpdateResultDo();

            var permission = (from p in this.db.Permissions
                              where p.PermissionCode == oPermission.PermissionCode
                              select p).FirstOrDefault();
            if (permission == null)
                result.AddError("E0075");
            else if (permission.UpdateDate > oPermission.LatestUpdateDate)
                result.AddError("E0006");

            if (result.HasError == false)
            {
                try
                {
                    this.db.Permissions.Remove(permission);
                    this.db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }

        public void UpdatePermissionSeq(PermissionSeqUpdateDo oPermission)
        {
            try
            {
                if (oPermission.Permissions.Count > 0)
                {
                    foreach (var pp in oPermission.Permissions)
                    {
                        var ups = (from p in this.db.Permissions

                                   orderby p.PermissionCode == pp.PermissionCode ? pp.NewSeqNo : p.SeqNo,
                                            p.SeqNo == pp.NewSeqNo ? pp.CurrentSeqNo < p.SeqNo ? -1 : 1 : 0

                                   select p);

                        int seqNo = 1;
                        foreach (var up in ups)
                        {
                            up.SeqNo = seqNo++;
                            up.UpdateDate = oPermission.UpdateDate;
                            up.UpdateBy = oPermission.UpdateBy;
                        }

                        this.db.SaveChanges();
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
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
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
