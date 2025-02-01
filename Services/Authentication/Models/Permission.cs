using Utils.Interfaces;

namespace Authentication.Models
{
    public class PermissionSearchCriteriaDo
    {
        public string Language { get; set; }
    }
    public class PermissionSearchResultDo : ASearchResultDo<PermissionSearchDo>
    {
    }
    public class PermissionSearchDo
    {
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public int SeqNo { get; set; }
        public bool ActiveFlag { get; set; }
        public bool PermissionUsed { get; set; }
    }
    public class PermissionDo
    {
        public string PermissionCode { get; set; }
        public string Description { get; set; }
        public bool ActiveFlag { get; set; }
        public List<PermissionNameDo> PermissionNames { get; set; }
        public DateTime? UpdateDate { get; set; }

        public PermissionDo()
        {
            this.PermissionNames = new List<PermissionNameDo>();
        }
    }
    public class PermissionNameDo
    {
        public string PermissionCode { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
    }

    public class PermissionSeqUpdateDo
    {
        public List<PermissionSeqDo> Permissions { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateBy { get; set; }

        public PermissionSeqUpdateDo()
        {
            this.Permissions = new List<PermissionSeqDo>();
        }

    }
    public class PermissionSeqDo
    {
        public string PermissionCode { get; set; }
        public int CurrentSeqNo { get; set; }
        public int NewSeqNo { get; set; }
    }
    public class PermissionUpdateDo
    {
        public string PermissionCode { get; set; }
        public string Description { get; set; }        
        public List<PermissionNameDo> PermissionNames { get; set; }
        public bool ActiveFlag { get; set; }
        public DateTime? LatestUpdateDate { get; set; }

        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }

        public PermissionUpdateDo()
        {
            this.PermissionNames = new List<PermissionNameDo>();
        }
    }
    public class PermissionUpdateResultDo: AUpdateTransactionDo<PermissionDo>
    {
    }
}
