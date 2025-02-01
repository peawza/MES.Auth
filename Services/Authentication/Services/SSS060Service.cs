using Authentication.Models;
using Authentication.Repositories;
using System.Security;

namespace Authentication.Services
{
    public partial interface ISSS060Service
    {
        ScreenSearchResultDo SearchScreen(ScreenSearchCriteriaDo oCriteria);
        ScreenDo? GetScreen(ScreenCriteriaDo oCriteria);
        ScreenUpdateResultDo AddScreen(ScreenUpdateDo oScreen);
        ScreenUpdateResultDo UpdateScreen(ScreenUpdateDo oScreen);
        ScreenUpdateResultDo DeleteScreen(ScreenUpdateDo oScreen);

        void UpdateScreenSeq(ScreenSeqUpdateDo oScreen);
    }

    public partial class SSS060Service : ISSS060Service
    {
        private readonly ISSS060Repository repository;
        
        public SSS060Service(
            ISSS060Repository repository
        )
        {
            this.repository = repository;
        }

        public ScreenSearchResultDo SearchScreen(ScreenSearchCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.SearchScreen(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ScreenDo? GetScreen(ScreenCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.GetScreen(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ScreenUpdateResultDo AddScreen(ScreenUpdateDo oScreen)
        {
            try
            {
                var result = this.repository.AddScreen(oScreen);
                if (result.HasError == false)
                {
                    result.Data = this.repository.GetScreen(new ScreenCriteriaDo()
                    {
                        AppCode = oScreen.AppCode,
                        ScreenId = oScreen.ScreenId,
                        Language = oScreen.Language
                    });
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ScreenUpdateResultDo UpdateScreen(ScreenUpdateDo oScreen)
        {
            try
            {
                var result = this.repository.UpdateScreen(oScreen);
                if (result.HasError == false)
                {
                    result.Data = this.repository.GetScreen(new ScreenCriteriaDo()
                    {
                        AppCode = oScreen.AppCode,
                        ScreenId = oScreen.ScreenId,
                        Language = oScreen.Language
                    });
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ScreenUpdateResultDo DeleteScreen(ScreenUpdateDo oScreen)
        {
            try
            {
                return this.repository.DeleteScreen(oScreen);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateScreenSeq(ScreenSeqUpdateDo oScreen)
        {
            try
            {
                this.repository.UpdateScreenSeq(oScreen);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
