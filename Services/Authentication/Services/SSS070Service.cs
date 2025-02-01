using Authentication.Models;
using Authentication.Repositories;
using System.Security;

namespace Authentication.Services
{
    public partial interface ISSS070Service
    {
        MenuSearchResultDo SearchMenu(MenuSearchCriteriaDo oCriteria);
        ScreenSearchResultDo SearchScreenForMenu(ScreenSearchCriteriaDo oCriteria);
        MenuDo? GetMenu(MenuCriteriaDo oCriteria);
        MenuUpdateResultDo AddMenu(MenuUpdateDo oMenu);
        MenuUpdateResultDo UpdateMenu(MenuUpdateDo oMenu);
        MenuUpdateResultDo DeleteMenu(MenuUpdateDo oMenu);
        void UpdateMenuSeq(MenuSeqUpdateDo oMenu);
    }

    public partial class SSS070Service : ISSS070Service
    {
        private readonly ISSS070Repository repository;
        
        public SSS070Service(
            ISSS070Repository repository
        )
        {
            this.repository = repository;
        }

        public MenuSearchResultDo SearchMenu(MenuSearchCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.SearchMenu(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ScreenSearchResultDo SearchScreenForMenu(ScreenSearchCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.SearchScreenForMenu(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MenuDo? GetMenu(MenuCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.GetMenu(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MenuUpdateResultDo AddMenu(MenuUpdateDo oMenu)
        {
            try
            {
                var result = this.repository.AddMenu(oMenu);
                if (result.HasError == false)
                {
                    result.Data = this.repository.GetMenu(new MenuCriteriaDo()
                    {
                        AppCode = result.Data.AppCode,
                        MenuId = result.Data.MenuId
                    });
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MenuUpdateResultDo UpdateMenu(MenuUpdateDo oMenu)
        {
            try
            {
                var result = this.repository.UpdateMenu(oMenu);
                if (result.HasError == false)
                {
                    result.Data = this.repository.GetMenu(new MenuCriteriaDo()
                    {
                        AppCode = oMenu.AppCode,
                        MenuId = oMenu.MenuId
                    });
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MenuUpdateResultDo DeleteMenu(MenuUpdateDo oMenu)
        {
            try
            {
                return this.repository.DeleteMenu(oMenu);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateMenuSeq(MenuSeqUpdateDo oMenu)
        {
            try
            {
                this.repository.UpdateMenuSeq(oMenu);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
