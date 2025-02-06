using Authentication.Repositories;
using static Authentication.Models.MESScreenModel;

namespace Authentication.Services
{
    public partial interface IMESScreenService
    {
        Task<List<MESScreenResult>>? getMesScreen(MESScreenCriteria criteria);
    }

    public partial class MESScreenService : IMESScreenService
    {
        private readonly IMESScreenRepository repository;

        public MESScreenService(
            IMESScreenRepository repository
        )
        {
            this.repository = repository;
        }


        public async Task<List<MESScreenResult>>? getMesScreen(MESScreenCriteria criteria)
        {
            try
            {
                return await this.repository.getMesScreen(criteria);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
