using Authentication.Models;
using Authentication.Repositories;

namespace Authentication.Services
{
    public partial interface IResouresService
    {
        Task<List<LocalizedMessagesResult>>? LocalizedMessages(LocalizedMessagesCriteria criteria);

        List<LocalizedResourcesResult>? LocalizedResources(LocalizedResourcesCriteria criteria);
    }

    public partial class ResouresService : IResouresService
    {
        private readonly IResourcesRepository repository;

        public ResouresService(
            IResourcesRepository repository
        )
        {
            this.repository = repository;
        }

        public async Task<List<LocalizedMessagesResult>>? LocalizedMessages(LocalizedMessagesCriteria criteria)
        {
            try
            {
                return await this.repository.LocalizedMessages(criteria);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<LocalizedResourcesResult>? LocalizedResources(LocalizedResourcesCriteria criteria)
        {
            try
            {
                return this.repository.LocalizedResources(criteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
