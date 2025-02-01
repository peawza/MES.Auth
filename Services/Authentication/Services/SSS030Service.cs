using Authentication.Models;
using Authentication.Repositories;

namespace Authentication.Services
{
    public partial interface ISSS030Service
    {
        UserSearchResultDo SearchUser(UserSearchCriteriaDo oCriteria);
        UserDo GetUser(UserCriteriaDo oCriteria);
        string ValidateUser(UpdateUserDo oUser);
    }

    public partial class SSS030Service : ISSS030Service
    {
        private readonly ISSS030Repository repository;
        
        public SSS030Service(
            ISSS030Repository repository
        )
        {
            this.repository = repository;
        }

        public UserSearchResultDo SearchUser(UserSearchCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.SearchUser(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserDo GetUser(UserCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.GetUser(oCriteria);
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
                return this.repository.ValidateUser(oUser);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
