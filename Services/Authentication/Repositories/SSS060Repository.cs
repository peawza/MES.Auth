using Application;
using Application.Models;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories
{
    public interface ISSS060Repository
    {
        ScreenSearchResultDo SearchScreen(ScreenSearchCriteriaDo oCriteria);
        ScreenDo? GetScreen(ScreenCriteriaDo oCriteria);
        ScreenUpdateResultDo AddScreen(ScreenUpdateDo oScreen);
        ScreenUpdateResultDo UpdateScreen(ScreenUpdateDo oScreen);
        ScreenUpdateResultDo DeleteScreen(ScreenUpdateDo oScreen);

        void UpdateScreenSeq(ScreenSeqUpdateDo oScreen);
    }

    public class SSS060Repository : ISSS060Repository, IDisposable
    {
        private SystemDbContext db { get; set; }

        public SSS060Repository(SystemDbContext db)
        {
            this.db = db;
        }

        public ScreenSearchResultDo SearchScreen(ScreenSearchCriteriaDo oCriteria)
        {
            try
            {
                ScreenSearchResultDo result = new ScreenSearchResultDo();

                var screens = (from s in this.db.Screens.AsNoTracking()
                               join sn in this.db.ScreenNames.AsNoTracking()
                                    on new { s.AppCode, s.ScreenId, oCriteria.Language }
                                        equals new { sn.AppCode, sn.ScreenId, sn.Language }

                               join _sms in (from ms in this.db.MenuSettings.AsNoTracking()
                                             where ms.AppCode == oCriteria.AppCode
                                             select new
                                             {
                                                 AppCode = ms.AppCode,
                                                 ScreenId = ms.ScreenId
                                             }).Distinct()
                                    on new { s.AppCode, s.ScreenId }
                                        equals new { _sms.AppCode, _sms.ScreenId } into g_sms
                               from sms in g_sms.DefaultIfEmpty()

                               where s.AppCode == oCriteria.AppCode

                               orderby s.SeqNo

                               select new ScreenSearchDo()
                               {
                                   AppCode = s.AppCode,
                                   ScreenId = s.ScreenId,
                                   ScreenName = sn.Name,
                                   ImageIcon = s.ImageIcon,
                                   Path = s.Path,
                                   SeqNo = s.SeqNo,
                                   ActiveFlag = s.ActiveFlag,
                                   ScreenUsed = sms != null
                               }).ToList();

                var permissions = (from s in screens
                                   join ssp in (from sp in this.db.ScreenPermissions.AsNoTracking()
                                                join p in this.db.Permissions.AsNoTracking()
                                                 on sp.PermissionCode equals p.PermissionCode
                                                join pn in this.db.PermissionNames.AsNoTracking()
                                                 on new { sp.PermissionCode, oCriteria.Language }
                                                     equals new { pn.PermissionCode, pn.Language }

                                                where p.ActiveFlag == true

                                                orderby sp.AppCode, sp.ScreenId, p.SeqNo

                                                select new ScreenSearchPermissionDo()
                                                {
                                                    AppCode = sp.AppCode,
                                                    ScreenId = sp.ScreenId,
                                                    PermissionCode = p.PermissionCode,
                                                    PermissionName = pn.Name,
                                                    SeqNo = p.SeqNo
                                                }).AsEnumerable()
                                    on new { s.AppCode, s.ScreenId }
                                        equals new { ssp.AppCode, ssp.ScreenId }
                                   select ssp).ToList();
                foreach (var s in screens)
                {
                    s.Permissions = permissions.FindAll((x) =>
                    {
                        return x.AppCode == s.AppCode
                                && x.ScreenId == s.ScreenId;
                    });
                }

                result.Rows = screens;
                result.TotalRecords = screens.Count;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ScreenDo? GetScreen(ScreenCriteriaDo oCriteria)
        {
            try
            {
                ScreenDo? screen = null;
                if (oCriteria.AppCode != null
                    && oCriteria.ScreenId != null)
                {
                    screen = (from s in this.db.Screens.AsNoTracking()

                              where s.AppCode == oCriteria.AppCode
                                    && s.ScreenId == oCriteria.ScreenId

                              select new ScreenDo()
                              {
                                  AppCode = s.AppCode,
                                  ScreenId = s.ScreenId,
                                  ImageIcon = s.ImageIcon,
                                  Path = s.Path,
                                  ActiveFlag = s.ActiveFlag,
                                  UpdateDate = s.UpdateDate
                              }).FirstOrDefault();
                    if (screen != null)
                    {
                        screen.ScreenNames = (from sn in this.db.ScreenNames.AsNoTracking()

                                              where sn.AppCode == oCriteria.AppCode
                                                    && sn.ScreenId == oCriteria.ScreenId

                                              select new ScreenNameDo()
                                              {
                                                  AppCode = sn.AppCode,
                                                  ScreenId = sn.ScreenId,
                                                  Language = sn.Language,
                                                  Name = sn.Name
                                              }).ToList();

                    }
                }
                if (screen == null)
                {
                    screen = new ScreenDo();
                    screen.ActiveFlag = true;
                }

                screen.ScreenPermissions = (from p in this.db.Permissions.AsNoTracking()
                                            join pn in this.db.PermissionNames.AsNoTracking()
                                             on new { p.PermissionCode, oCriteria.Language }
                                                 equals new { pn.PermissionCode, pn.Language }
                                            join _sp in this.db.ScreenPermissions.AsNoTracking()
                                             on new { oCriteria.AppCode, oCriteria.ScreenId, p.PermissionCode }
                                                equals new { _sp.AppCode, _sp.ScreenId, _sp.PermissionCode } into g_sp
                                            from sp in g_sp.DefaultIfEmpty()

                                            where p.ActiveFlag == true

                                            orderby p.SeqNo

                                            select new ScreenPermissionDo()
                                            {
                                                AppCode = oCriteria.AppCode,
                                                ScreenId = oCriteria.ScreenId,
                                                PermissionCode = p.PermissionCode,
                                                PermissionName = pn.Name,
                                                HasPermission = (sp != null)
                                            }).ToList();

                return screen;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ScreenUpdateResultDo AddScreen(ScreenUpdateDo oScreen)
        {
            ScreenUpdateResultDo result = new ScreenUpdateResultDo();

            if ((from s in this.db.Screens.AsNoTracking()
                 where s.AppCode == oScreen.AppCode
                        && s.ScreenId == oScreen.ScreenId
                 select s).FirstOrDefault() != null)
            {
                result.AddError(string.Format("E0013;{0}", oScreen.ScreenId));
            }
            if (result.HasError == false)
            {
                try
                {
                    List<tb_ScreenName> names = new List<tb_ScreenName>();
                    foreach (var sName in oScreen.ScreenNames)
                    {
                        names.Add(new tb_ScreenName()
                        {
                            AppCode = oScreen.AppCode,
                            ScreenId = oScreen.ScreenId,
                            Language = sName.Language,
                            Name = sName.Name
                        });
                    }

                    List<tb_ScreenPermission> permissions = new List<tb_ScreenPermission>();
                    foreach (var sp in oScreen.ScreenPermissions)
                    {
                        permissions.Add(new tb_ScreenPermission()
                        {
                            AppCode = oScreen.AppCode,
                            ScreenId = oScreen.ScreenId,
                            PermissionCode = sp.PermissionCode
                        });
                    }

                    int maxSeqNo = (from s in this.db.Screens.AsNoTracking()
                                    select s.SeqNo).DefaultIfEmpty().Max();

                    this.db.Screens.Add(new tb_Screen()
                    {
                        AppCode = oScreen.AppCode,
                        ScreenId = oScreen.ScreenId,
                        ImageIcon = oScreen.ImageIcon,
                        Path = oScreen.Path,
                        SeqNo = maxSeqNo + 1,
                        ActiveFlag = oScreen.ActiveFlag,
                        ScreenNames = names,
                        ScreenPermissions = permissions,
                        CreateDate = oScreen.CreateDate.Value,
                        CreateBy = oScreen.CreateBy.Value,
                        UpdateDate = oScreen.CreateDate.Value,
                        UpdateBy = oScreen.CreateBy.Value,
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
        public ScreenUpdateResultDo UpdateScreen(ScreenUpdateDo oScreen)
        {
            ScreenUpdateResultDo result = new ScreenUpdateResultDo();

            var screen = (from s in this.db.Screens
                          where s.AppCode == oScreen.AppCode
                                && s.ScreenId == oScreen.ScreenId
                          select s).FirstOrDefault();
            if (screen == null)
                result.AddError("E0075");
            else if (screen.UpdateDate > oScreen.LatestUpdateDate)
                result.AddError("E0006");

            if (result.HasError == false)
            {
                try
                {
                    screen.ImageIcon = oScreen.ImageIcon;
                    screen.Path = oScreen.Path;
                    screen.ActiveFlag = oScreen.ActiveFlag;
                    screen.UpdateDate = oScreen.UpdateDate.Value;
                    screen.UpdateBy = oScreen.UpdateBy.Value;

                    var screenNames = (from sn in this.db.ScreenNames
                                       where sn.AppCode == oScreen.AppCode
                                                && sn.ScreenId == oScreen.ScreenId
                                       select sn).ToList();
                    foreach (var sName in oScreen.ScreenNames)
                    {
                        var sn = screenNames
                            .Where(x => x.Language == sName.Language)
                            .FirstOrDefault();
                        if (sn != null)
                            sn.Name = sName.Name;
                        else
                        {
                            this.db.ScreenNames.Add(new tb_ScreenName()
                            {
                                AppCode = oScreen.AppCode,
                                ScreenId = oScreen.ScreenId,
                                Language = sName.Language,
                                Name = sName.Name
                            });
                        }
                    }

                    var screenPermissions = (from sp in this.db.ScreenPermissions
                                             where sp.AppCode == oScreen.AppCode
                                                      && sp.ScreenId == oScreen.ScreenId
                                             select sp).ToList();
                    foreach (var sp in screenPermissions)
                    {
                        if (oScreen.ScreenPermissions.Exists(x => x.PermissionCode == sp.PermissionCode) == false)
                        {
                            this.db.ScreenPermissions.Remove(sp);
                        }
                    }
                    foreach (var sp in oScreen.ScreenPermissions)
                    {
                        if (screenPermissions.Exists(x => x.PermissionCode == sp.PermissionCode) == false)
                        {
                            this.db.ScreenPermissions.Add(new tb_ScreenPermission()
                            {
                                AppCode = oScreen.AppCode,
                                ScreenId = oScreen.ScreenId,
                                PermissionCode = sp.PermissionCode
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
        public ScreenUpdateResultDo DeleteScreen(ScreenUpdateDo oScreen)
        {
            ScreenUpdateResultDo result = new ScreenUpdateResultDo();

            var screen = (from s in this.db.Screens
                          where s.AppCode == oScreen.AppCode
                                && s.ScreenId == oScreen.ScreenId
                          select s).FirstOrDefault();
            if (screen == null)
                result.AddError("E0075");
            else if (screen.UpdateDate > oScreen.LatestUpdateDate)
                result.AddError("E0006");

            if (result.HasError == false)
            {
                try
                {
                    this.db.Screens.Remove(screen);
                    this.db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }

        public void UpdateScreenSeq(ScreenSeqUpdateDo oScreen)
        {
            try
            {
                if (oScreen.Screens.Count > 0)
                {
                    foreach (var ss in oScreen.Screens)
                    {
                        var ups = (from s in this.db.Screens

                                   orderby (s.AppCode == ss.AppCode
                                            && s.ScreenId == ss.ScreenId) ? ss.NewSeqNo : s.SeqNo,
                                            s.SeqNo == ss.NewSeqNo ? ss.CurrentSeqNo < s.SeqNo ? -1 : 1 : 0

                                   select s);

                        int seqNo = 1;
                        foreach (var up in ups)
                        {
                            up.SeqNo = seqNo++;
                            up.UpdateDate = oScreen.UpdateDate;
                            up.UpdateBy = oScreen.UpdateBy;
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
