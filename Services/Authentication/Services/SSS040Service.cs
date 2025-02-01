using Authentication.Models;
using Authentication.Repositories;

namespace Authentication.Services
{
    public partial interface ISSS040Service
    {
        RoleSearchResultDo SearchRole(RoleSearchCriteriaDo oCriteria);
        RoleInfoDo GetRole(RoleCriteriaDo oCriteria);
        List<RoleScreenDo> GetScreensAllowList(RoleCriteriaDo oCriteria);
        string ValidateRole(RoleDo oRole);
        string ValidateRoleForDelete(RoleDo oRole);
        UserInRoleSearchResultDo SearchUserInRole(UserInRoleSearchCriteriaDo oCriteria);
    }

    public partial class SSS040Service : ISSS040Service
    {
        private readonly ISSS040Repository repository;
        
        public SSS040Service(
            ISSS040Repository repository
        )
        {
            this.repository = repository;
        }

        public RoleSearchResultDo SearchRole(RoleSearchCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.SearchRole(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public RoleInfoDo GetRole(RoleCriteriaDo oCriteria)
        {
            try
            {
                RoleInfoDo roleInfo = new RoleInfoDo();

                RoleDo role = this.repository.GetRole(oCriteria);
                if (role != null)
                {
                    roleInfo.RoleId = role.RoleId;
                    roleInfo.RoleName = role.RoleName;
                    roleInfo.Description = role.Description;
                    roleInfo.ActiveFlag = role.ActiveFlag;
                    roleInfo.UpdateDate = role.UpdateDate;
                    roleInfo.UpdateByName = role.UpdateByName;
                }
                else
                {
                    roleInfo.ActiveFlag = true;
                }

                roleInfo.Screens= this.repository.GetScreens(oCriteria);

                return roleInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ValidateRole(RoleDo oRole)
        {
            try
            {
                return this.repository.ValidateRole(oRole);
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
                return this.repository.ValidateRoleForDelete(oRole);
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
                return this.repository.SearchUserInRole(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<RoleScreenDo> GetScreensAllowList(RoleCriteriaDo oCriteria)
        {
            try
            {

                return this.repository.GetScreensAllowList(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
