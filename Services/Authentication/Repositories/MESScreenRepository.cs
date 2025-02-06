using Application;
using Microsoft.EntityFrameworkCore;
using static Authentication.Models.MESScreenModel;

namespace Authentication.Repositories
{
    public interface IMESScreenRepository
    {

        Task<List<MESScreenResult>>? getMesScreen(MESScreenCriteria criteria);
    }

    public class MESScreenRepository : IMESScreenRepository, IDisposable
    {
        private MESSystemDbContext db { get; set; }

        public MESScreenRepository(MESSystemDbContext db)
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

        public async Task<List<MESScreenResult>>? getMesScreen(MESScreenCriteria criteria)
        {
            try
            {

                List<MESScreenResult> result = await (from screen in db.Screen
                                                      join module in db.Module on screen.ModuleCode equals module.ModuleCode
                                                      join subModule in db.SubModule on screen.SubModuleCode equals subModule.SubModuleCode

                                                      where screen.SupportDeviceType.Contains(criteria.SupportDeviceType)
                                                      select new MESScreenResult
                                                      {
                                                          ScreenId = screen.ScreenId,
                                                          Name_EN = screen.Name_EN,
                                                          Name_TH = screen.Name_TH,
                                                          FunctionCode = screen.FunctionCode,
                                                          ModuleCode = module.ModuleCode,
                                                          ModuleName_EN = module.ModuleName_EN,
                                                          ModuleName_TH = module.ModuleName_TH,
                                                          ModuleName_Seq = module.Seq,
                                                          ModuleName_IconClass = module.IconClass,
                                                          SubModuleCode = subModule.SubModuleCode,
                                                          SubModuleName_EN = subModule.SubModuleName_EN,
                                                          SubModuleName_TH = subModule.SubModuleName_TH,
                                                          SubModule_IconClass = subModule.IconClass,
                                                          Screen_IconClass = screen.IconClass,
                                                          Screen_MainMenuFlag = screen.MainMenuFlag,
                                                          Screen_PermissionFlag = screen.PermissionFlag,
                                                          Screen_Seq = screen.Seq
                                                      }).ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
