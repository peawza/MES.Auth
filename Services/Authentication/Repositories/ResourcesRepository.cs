using Application;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories
{
    public interface IResourcesRepository
    {
        Task<List<LocalizedMessagesResult>>? LocalizedMessages(LocalizedMessagesCriteria criteria);
        List<LocalizedResourcesResult>? LocalizedResources(LocalizedResourcesCriteria criteria);

    }

    public class ResourcesRepository : IResourcesRepository, IDisposable
    {
        private SystemDbContext db { get; set; }

        public ResourcesRepository(SystemDbContext db)
        {
            this.db = db;
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

        public async Task<List<LocalizedMessagesResult>>? LocalizedMessages(LocalizedMessagesCriteria criteria)
        {
            try
            {
                List<LocalizedMessagesResult> result = await db.LocalizedMessages.Select(lr => new LocalizedMessagesResult
                {
                    MessageCode = lr.MessageCode,
                    MessageType = lr.MessageType,
                    MessageNameEN = lr.MessageNameEN,
                    MessageNameTH = lr.MessageNameTH,
                    Remark = lr.Remark,
                    CreateDate = lr.CreateDate,
                    CreateBy = lr.CreateBy
                })
                .ToListAsync();

                return result;
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
                List<LocalizedResourcesResult> result = db.LocalizedResources.Select(lr => new LocalizedResourcesResult
                {
                    ScreenCode = lr.ScreenCode,
                    ObjectID = lr.ObjectID,
                    ResourcesEN = lr.ResourcesEN,
                    ResourcesTH = lr.ResourcesTH,
                    Remark = lr.Remark,
                    CreateDate = lr.CreateDate,
                    CreateBy = lr.CreateBy
                })
                .ToList();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
