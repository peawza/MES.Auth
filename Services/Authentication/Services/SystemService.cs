using Authentication.Models;
using Authentication.Repositories;

namespace Authentication.Services
{
    public partial interface ISystemService
    {
        ApplicationDo? GetApplication(ApplicationDo oCriteria);
        List<UserInfoDo> GetUsers(UserInfoCriteriaDo oCriteria);
        List<RoleDo> GetRoles(RoleCriteriaDo oCriteria);
        ScreenInfoDo GetScreenInfo(ScreenInfoCriteriaDo criteria);
    }

    public partial class SystemService : ISystemService
    {
        private readonly ISystemRepository repository;
        
        public SystemService(
            ISystemRepository repository
        )
        {
            this.repository = repository;
        }

        public ApplicationDo? GetApplication(ApplicationDo oCriteria)
        {
            try
            {
                return this.repository.GetApplication(oCriteria);
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
                return this.repository.GetUsers(oCriteria);
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
                return this.repository.GetRoles(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ScreenInfoDo GetScreenInfo(ScreenInfoCriteriaDo criteria)
        {
            try
            {
                ScreenInfoDo info = new ScreenInfoDo();
                info.Screens = this.repository.GetScreens(criteria);
                info.Menus = this.repository.GetMenus(criteria);

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
