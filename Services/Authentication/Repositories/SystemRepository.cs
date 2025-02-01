using Application;
using Authentication.Constants;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories
{
    public interface ISystemRepository
    {
        ApplicationDo? GetApplication(ApplicationDo oCriteria);
        List<UserInfoDo> GetUsers(UserInfoCriteriaDo oCriteria);
        List<RoleDo> GetRoles(RoleCriteriaDo oCriteria);
        List<ScreenDisplayDo> GetScreens(ScreenInfoCriteriaDo oCriteria);
        List<MenuDisplayDo> GetMenus(ScreenInfoCriteriaDo oCriteria);
    }

    public class SystemRepository : ISystemRepository, IDisposable
    {
        private SystemDbContext db { get; set; }
        private ApplicationDbContext appDb { get; set; }

        public SystemRepository(SystemDbContext db, ApplicationDbContext appDb)
        {
            this.db = db;
            this.appDb = appDb;
        }

        public ApplicationDo? GetApplication(ApplicationDo oCriteria)
        {
            try
            {
                return (from a in this.db.Applications.AsNoTracking()
                        where a.AppCode == oCriteria.AppCode
                                && a.ActiveFlag == true
                        select new ApplicationDo()
                        {
                            AppCode = a.AppCode,
                            AppName = a.AppName
                        }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<UserInfoDo> GetUsers(UserInfoCriteriaDo oCriteria)
        {
            try
            {
                return (from u in this.appDb.Users
                        join ui in this.appDb.UserInfos.AsNoTracking()
                            on u.Id equals ui.Id

                        where u.ActiveFlag == true
                                && (oCriteria.Name == null
                                    || (ui.UserName + ": " + ui.FirstName + " " + ui.LastName).Contains(oCriteria.Name))

                        orderby u.UserName

                        select new UserInfoDo()
                        {
                            UserName = u.UserName,
                            Name = ui.FirstName + " " + ui.LastName,
                            NameWithCode = ui.UserName + ": " + ui.FirstName + " " + ui.LastName
                        }).Take(100).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<RoleDo> GetRoles(RoleCriteriaDo oCriteria)
        {
            try
            {
                return (from r in this.appDb.ApplicationRoles.AsNoTracking()
                        where r.ActiveFlag == true
                                && r.AppCode == oCriteria.AppCode
                        orderby r.Name
                        select new RoleDo()
                        {
                            RoleId = r.Id,
                            Name = r.Name,
                            RoleName = r.RoleName
                        }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ScreenDisplayDo> GetScreens(ScreenInfoCriteriaDo oCriteria)
        {
            try
            {
                return (from ss in
                            (from s in this.db.Screens.AsNoTracking()
                             join sn in this.db.ScreenNames.AsNoTracking()
                                 on s.ScreenId equals sn.ScreenId
                             where s.AppCode == oCriteria.AppCode
                                     && sn.Language == oCriteria.Language

                             select new ScreenDisplayDo()
                             {
                                 ScreenId = s.ScreenId,
                                 ImageIcon = s.ImageIcon,
                                 ScreenName = sn.Name,
                                 Path = s.Path
                             }).ToList()
                        join cs in oCriteria.Screens
                            on ss.ScreenId equals cs
                        select ss).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MenuDisplayDo> GetMenus(ScreenInfoCriteriaDo oCriteria)
        {
            List<MenuDisplayDo> menus = new List<MenuDisplayDo>();

            try
            {
                var screens = (from s in this.db.Screens.AsNoTracking().AsEnumerable()
                               join cs in oCriteria.Screens
                                 on s.ScreenId equals cs
                               where s.AppCode == oCriteria.AppCode
                               select s).ToList();

                List<MenuDisplayDo> dbResults =
                    (from m in (from ms in this.db.MenuSettings.AsNoTracking()
                                join mpn in this.db.MenuNames.AsNoTracking()
                                   on ms.MenuId equals mpn.MenuId
                                where ms.ActiveFlag == true
                                        && ms.AppCode == oCriteria.AppCode
                                select new
                                {
                                    MenuId = ms.MenuId,
                                    MenuType = ms.MenuType,
                                    MenuName = mpn.Name,
                                    ParentMenuId = ms.ParentMenuId,
                                    ScreenId = ms.ScreenId,
                                    MenuURL = ms.MenuURL,
                                    ImageIcon = ms.ImageIcon,
                                    SeqNo = ms.SeqNo,
                                    Language = mpn.Language
                                }).AsEnumerable()

                     join fs in screens
                         on m.ScreenId equals fs.ScreenId into fs
                     from dfs in fs.DefaultIfEmpty()

                     where (m.ScreenId == null
                                 || (m.ScreenId != null
                                     && dfs != null ? dfs.ScreenId != null : false))
                            && m.Language == oCriteria.Language
                     orderby m.ParentMenuId, m.SeqNo
                     select new MenuDisplayDo()
                     {
                         MenuId = m.MenuId,
                         MenuType = m.MenuType,
                         MenuName = m.MenuName,
                         ParentMenuId = m.ParentMenuId,
                         ScreenId = m.ScreenId,
                         MenuURL = dfs != null ? dfs.Path : m.MenuURL,
                         ImageIcon = dfs != null ? dfs.ImageIcon : m.ImageIcon,
                         SeqNo = m.SeqNo
                     }
                    ).ToList();

                MenuDisplayDo mg = MenuGrouping(dbResults);
                menus = mg.Childrens;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return menus;
        }

        private MenuDisplayDo MenuGrouping(List<MenuDisplayDo> dbs, MenuDisplayDo m = null)
        {
            if (m == null)
                m = new MenuDisplayDo();

            foreach (var row in dbs.Where(x => (m.MenuId == 0 ? (x.ParentMenuId == null) : (x.ParentMenuId == m.MenuId)))
                                   .OrderBy(x => x.SeqNo))
            {
                MenuDisplayDo menu = MenuGrouping(dbs, row);
                if (menu.MenuType == MENU.MENU_TYPE_GROUP
                        && menu.Childrens.Count == 0)
                    continue;

                m.Childrens.Add(menu);
            }

            return m;
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
