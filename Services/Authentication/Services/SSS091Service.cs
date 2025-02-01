using Authentication.Models;
using Authentication.Repositories;
using System.Security;

namespace Authentication.Services
{
    public partial interface ISSS091Service
    {
        List<UserLogTrailDo>? getUserLogTrail(UserLogTrailCriteriaDo oCriteria);
    }

    public partial class SSS091Service : ISSS091Service
    {
        private readonly ISSS091Repository repository;

        public SSS091Service(
            ISSS091Repository repository
        )
        {
            this.repository = repository;
        }

        public List<UserLogTrailDo>? getUserLogTrail(UserLogTrailCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.GetUerLogTrails(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

    