using Application;
using Application.Models;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories
{
    public interface ISSS070Repository
    {
        MenuSearchResultDo SearchMenu(MenuSearchCriteriaDo oCriteria);
        ScreenSearchResultDo SearchScreenForMenu(ScreenSearchCriteriaDo oCriteria);
        MenuDo? GetMenu(MenuCriteriaDo oCriteria);
        MenuUpdateResultDo AddMenu(MenuUpdateDo oMenu);
        MenuUpdateResultDo UpdateMenu(MenuUpdateDo oMenu);
        MenuUpdateResultDo DeleteMenu(MenuUpdateDo oMenu);
        void UpdateMenuSeq(MenuSeqUpdateDo oMenu);
    }

    public class SSS070Repository : ISSS070Repository, IDisposable
    {
        private SystemDbContext db { get; set; }

        public SSS070Repository(SystemDbContext db)
        {
            this.db = db;
        }

        public MenuSearchResultDo SearchMenu(MenuSearchCriteriaDo oCriteria)
        {
            try
            {
                MenuSearchResultDo result = new MenuSearchResultDo();


                List<MenuSearchDo> dbResults =
                    (from ms in this.db.MenuSettings.AsNoTracking()

                     join mpn in this.db.MenuNames.AsNoTracking()
                        on new { ms.AppCode, ms.MenuId, oCriteria.Language }
                         equals new { mpn.AppCode, mpn.MenuId, mpn.Language }

                     join _s in this.db.Screens.AsNoTracking()
                         on new { ms.AppCode, ms.ScreenId }
                             equals new { _s.AppCode, _s.ScreenId } into g_s
                     from s in g_s.DefaultIfEmpty()

                     where ms.AppCode == oCriteria.AppCode

                     select new MenuSearchDo()
                     {
                         AppCode = ms.AppCode,
                         MenuId = ms.MenuId,
                         MenuType = ms.MenuType,
                         MenuName = mpn.Name,
                         ParentMenuId = ms.ParentMenuId,
                         ScreenId = ms.ScreenId,
                         MenuURL = (s != null ? s.Path : ms.MenuURL),
                         ImageIcon = (s != null ? s.ImageIcon : ms.ImageIcon),
                         SeqNo = ms.SeqNo,
                         ActiveFlag = ms.ActiveFlag
                     }).ToList();

                MenuSearchDo mg = MenuGrouping(dbResults);
                result.Rows = mg.Childrens;
                result.TotalRecords = mg.Childrens.Count;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private MenuSearchDo MenuGrouping(List<MenuSearchDo> dbs, MenuSearchDo m = null)
        {
            if (m == null)
                m = new MenuSearchDo();

            foreach (var row in dbs.Where(x => (m.MenuId == 0 ? (x.ParentMenuId == null) : (x.ParentMenuId == m.MenuId)))
                                   .OrderBy(x => x.SeqNo))
            {
                MenuSearchDo menu = MenuGrouping(dbs, row);
                m.Childrens.Add(menu);
            }

            return m;
        }
        public ScreenSearchResultDo SearchScreenForMenu(ScreenSearchCriteriaDo oCriteria)
        {
            try
            {
                ScreenSearchResultDo result = new ScreenSearchResultDo();

                var screens = (from s in this.db.Screens.AsNoTracking()
                               join sn in this.db.ScreenNames.AsNoTracking()
                                    on new { s.AppCode, s.ScreenId, oCriteria.Language }
                                        equals new { sn.AppCode, sn.ScreenId, sn.Language }

                               where s.AppCode == oCriteria.AppCode
                                        && s.ActiveFlag == true

                               orderby s.SeqNo

                               select new ScreenSearchDo()
                               {
                                   AppCode = s.AppCode,
                                   ScreenId = s.ScreenId,
                                   ScreenName = sn.Name,
                                   ImageIcon = s.ImageIcon,
                                   Path = s.Path,
                                   SeqNo = s.SeqNo,
                                   ActiveFlag = s.ActiveFlag
                               }).ToList();

                result.Rows = screens;
                result.TotalRecords = screens.Count;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MenuDo? GetMenu(MenuCriteriaDo oCriteria)
        {
            try
            {
                var menu = (from m in this.db.MenuSettings.AsNoTracking()

                            where m.AppCode == oCriteria.AppCode
                                  && m.MenuId == oCriteria.MenuId

                            select new MenuDo()
                            {
                                AppCode = m.AppCode,
                                MenuId = m.MenuId,
                                MenuType = m.MenuType,
                                ParentMenuId = m.ParentMenuId,
                                ScreenId = m.ScreenId,
                                MenuURL = m.MenuURL,
                                ImageIcon = m.ImageIcon,
                                ActiveFlag = m.ActiveFlag,
                                UpdateDate = m.UpdateDate
                            }).FirstOrDefault();
                if (menu != null)
                {
                    menu.MenuNames = (from mn in this.db.MenuNames.AsNoTracking()

                                      where mn.AppCode == oCriteria.AppCode
                                            && mn.MenuId == oCriteria.MenuId

                                      select new MenuNameDo()
                                      {
                                          AppCode = mn.AppCode,
                                          MenuId = mn.MenuId,
                                          Language = mn.Language,
                                          Name = mn.Name
                                      }).ToList();
                }

                return menu;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MenuUpdateResultDo AddMenu(MenuUpdateDo oMenu)
        {
            MenuUpdateResultDo result = new MenuUpdateResultDo();

            try
            {
                if (oMenu.ScreenId != null)
                {
                    oMenu.MenuNames = (from sn in this.db.ScreenNames.AsNoTracking()
                                       where sn.AppCode == oMenu.AppCode
                                                && sn.ScreenId == oMenu.ScreenId
                                       select new MenuNameDo()
                                       {
                                           AppCode = oMenu.AppCode,
                                           Language = sn.Language,
                                           Name = sn.Name
                                       }).ToList();
                }

                int maxMenuId = (from ms in this.db.MenuSettings
                                 where ms.AppCode == oMenu.AppCode
                                 select ms).DefaultIfEmpty().Max(x => x != null ? x.MenuId : 0);

                List<tb_MenuName> names = new List<tb_MenuName>();
                foreach (var sName in oMenu.MenuNames)
                {
                    names.Add(new tb_MenuName()
                    {
                        AppCode = oMenu.AppCode,
                        MenuId = maxMenuId + 1,
                        Language = sName.Language,
                        Name = sName.Name
                    });
                }

                int maxSeqNo = (from ms in this.db.MenuSettings.AsNoTracking()
                                where ms.ParentMenuId == oMenu.ParentMenuId
                                select ms.SeqNo).DefaultIfEmpty().Max();

                var newMenu = new tb_MenuSetting()
                {
                    AppCode = oMenu.AppCode,
                    MenuId = maxMenuId + 1,
                    MenuType = oMenu.MenuType,
                    ParentMenuId = oMenu.ParentMenuId,
                    SeqNo = maxSeqNo + 1,
                    ScreenId = oMenu.ScreenId,
                    ImageIcon = oMenu.ScreenId == null ? oMenu.ImageIcon : null,
                    MenuURL = oMenu.ScreenId == null ? oMenu.MenuURL : null,
                    ActiveFlag = oMenu.ActiveFlag,
                    MenuNames = names,
                    CreateDate = oMenu.CreateDate.Value,
                    CreateBy = oMenu.CreateBy.Value,
                    UpdateDate = oMenu.CreateDate.Value,
                    UpdateBy = oMenu.CreateBy.Value,
                };
                this.db.MenuSettings.Add(newMenu);

                this.db.SaveChanges();

                result.Data = new MenuDo()
                {
                    AppCode = newMenu.AppCode,
                    MenuId = newMenu.MenuId
                };
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public MenuUpdateResultDo UpdateMenu(MenuUpdateDo oMenu)
        {
            MenuUpdateResultDo result = new MenuUpdateResultDo();

            var menu = (from ms in this.db.MenuSettings
                        where ms.AppCode == oMenu.AppCode
                              && ms.MenuId == oMenu.MenuId
                        select ms).FirstOrDefault();
            if (menu == null)
                result.AddError("E0075");
            else if (menu.UpdateDate > oMenu.LatestUpdateDate)
                result.AddError("E0006");

            if (result.HasError == false)
            {
                try
                {
                    menu.MenuType = oMenu.MenuType;
                    menu.ParentMenuId = oMenu.ParentMenuId;
                    menu.ImageIcon = oMenu.ScreenId == null ? oMenu.ImageIcon : null;
                    menu.MenuURL = oMenu.ScreenId == null ? oMenu.MenuURL : null;
                    menu.ScreenId = oMenu.ScreenId;
                    menu.ActiveFlag = oMenu.ActiveFlag;
                    menu.UpdateDate = oMenu.UpdateDate.Value;
                    menu.UpdateBy = oMenu.UpdateBy.Value;

                    if (oMenu.ScreenId != null)
                    {
                        oMenu.MenuNames = (from sn in this.db.ScreenNames.AsNoTracking()
                                           where sn.AppCode == oMenu.AppCode
                                                    && sn.ScreenId == oMenu.ScreenId
                                           select new MenuNameDo()
                                           {
                                               AppCode = oMenu.AppCode,
                                               Language = sn.Language,
                                               Name = sn.Name
                                           }).ToList();
                    }

                    var menuNames = (from mn in this.db.MenuNames
                                     where mn.AppCode == oMenu.AppCode
                                              && mn.MenuId == oMenu.MenuId
                                     select mn).ToList();
                    foreach (var mName in oMenu.MenuNames)
                    {
                        var mn = menuNames
                            .Where(x => x.Language == mName.Language)
                            .FirstOrDefault();
                        if (mn != null)
                            mn.Name = mName.Name;
                        else
                        {
                            this.db.MenuNames.Add(new tb_MenuName()
                            {
                                AppCode = oMenu.AppCode,
                                MenuId = oMenu.MenuId.Value,
                                Language = mName.Language,
                                Name = mName.Name
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
        public MenuUpdateResultDo DeleteMenu(MenuUpdateDo oMenu)
        {
            MenuUpdateResultDo result = new MenuUpdateResultDo();

            var menu = (from ms in this.db.MenuSettings
                        where ms.AppCode == oMenu.AppCode
                              && ms.MenuId == oMenu.MenuId
                        select ms).FirstOrDefault();
            if (menu == null)
                result.AddError("E0075");
            else if (menu.UpdateDate > oMenu.LatestUpdateDate)
                result.AddError("E0006");

            if (result.HasError == false)
            {
                try
                {
                    this.db.MenuSettings.Remove(menu);
                    this.db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }

        public void UpdateMenuSeq(MenuSeqUpdateDo oMenu)
        {
            try
            {
                if (oMenu.Menus.Count > 0)
                {
                    var ms = (from m in this.db.MenuSettings
                              where m.AppCode == oMenu.AppCode
                              select m).ToList();

                    foreach (var nms in oMenu.Menus)
                    {
                        var nm = ms.Where(x => x.MenuId == nms.MenuId).FirstOrDefault();
                        if (nm != null)
                        {
                            if (nm.SeqNo != nms.SeqNo)
                            {
                                nm.SeqNo = nms.SeqNo;
                                nm.UpdateDate = oMenu.UpdateDate;
                                nm.UpdateBy = oMenu.UpdateBy;
                            }
                        }
                    }

                    this.db.SaveChanges();
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
