using Authentication.Models;
using Authentication.Repositories;
using System.Security;

namespace Authentication.Services
{
    public partial interface ISSS050Service
    {
        PermissionSearchResultDo SearchPermission(PermissionSearchCriteriaDo oCriteria);
        PermissionDo? GetPermission(PermissionDo oCriteria);
        PermissionUpdateResultDo AddPermission(PermissionUpdateDo oPermission);
        PermissionUpdateResultDo UpdatePermission(PermissionUpdateDo oPermission);
        PermissionUpdateResultDo DeletePermission(PermissionUpdateDo oPermission);
        void UpdatePermissionSeq(PermissionSeqUpdateDo oPermission);
    }

    public partial class SSS050Service : ISSS050Service
    {
        private readonly ISSS050Repository repository;
        
        public SSS050Service(
            ISSS050Repository repository
        )
        {
            this.repository = repository;
        }

        public PermissionSearchResultDo SearchPermission(PermissionSearchCriteriaDo oCriteria)
        {
            try
            {
                return this.repository.SearchPermission(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PermissionDo? GetPermission(PermissionDo oCriteria)
        {
            try
            {
                return this.repository.GetPermission(oCriteria);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PermissionUpdateResultDo AddPermission(PermissionUpdateDo oPermission)
        {
            try
            {
                var result = this.repository.AddPermission(oPermission);
                if (result.HasError == false)
                {
                    result.Data = this.repository.GetPermission(new PermissionDo()
                    {
                        PermissionCode = oPermission.PermissionCode
                    });
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PermissionUpdateResultDo UpdatePermission(PermissionUpdateDo oPermission)
        {
            try
            {
                var result = this.repository.UpdatePermission(oPermission);
                if (result.HasError == false)
                {
                    result.Data = this.repository.GetPermission(new PermissionDo()
                    {
                        PermissionCode = oPermission.PermissionCode
                    });
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PermissionUpdateResultDo DeletePermission(PermissionUpdateDo oPermission)
        {
            try
            {
                return this.repository.DeletePermission(oPermission);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdatePermissionSeq(PermissionSeqUpdateDo oPermission)
        {
            try
            {
                this.repository.UpdatePermissionSeq(oPermission);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
