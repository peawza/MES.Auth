using Utils.Interfaces;

namespace Authentication.Models
{
    public class ScreenInfoCriteriaDo
    {
        public string AppCode { get; set; }
        public string Language { get; set; }
        public List<string> Screens { get; set; }

        public ScreenInfoCriteriaDo()
        {
            this.Screens = new List<string>();
        }
    }
    public class ScreenDisplayDo
    {
        public string ScreenId { get; set; }
        public string ImageIcon { get; set; }
        public string ScreenName { get; set; }
        public string Path { get; set; }
    }
    public class ScreenInfoDo
    {
        public List<ScreenDisplayDo> Screens { get; set; }
        public List<MenuDisplayDo> Menus { get; set; }
    }

    public class ScreenSearchCriteriaDo : ASearchCriteriaDo
    {
        public string AppCode { get; set; }
        public string Language { get; set; }
    }
    public class ScreenSearchResultDo: ASearchResultDo<ScreenSearchDo>
    {
    }
    public class ScreenSearchDo
    {
        public string AppCode { get; set; }
        public string ScreenId { get; set; }
        public string ScreenName { get; set; }
        public string ImageIcon { get; set; }
        public string Path { get; set; }
        public int SeqNo { get; set; }
        public bool ActiveFlag { get; set; }
        public bool ScreenUsed { get; set; }

        public List<ScreenSearchPermissionDo> Permissions { get; set; }

        public ScreenSearchDo()
        {
            this.Permissions = new List<ScreenSearchPermissionDo>();
        }
    }
    public class ScreenSearchPermissionDo
    {
        public string AppCode { get; set; }
        public string ScreenId { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public int SeqNo { get; set; }
    }

    public class ScreenSeqUpdateDo
    {
        public List<ScreenSeqDo> Screens { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateBy { get; set; }

        public ScreenSeqUpdateDo()
        {
            this.Screens = new List<ScreenSeqDo>();
        }

    }
    public class ScreenSeqDo
    {
        public string AppCode { get; set; }
        public string ScreenId { get; set; }
        public int CurrentSeqNo { get; set; }
        public int NewSeqNo { get; set; }
    }

    public class ScreenCriteriaDo
    {
        public string AppCode { get; set; }
        public string ScreenId { get; set; }
        public string Language { get; set; }
    }
    public class ScreenDo
    {
        public string AppCode { get; set; }
        public string ScreenId { get; set; }
        public string ImageIcon { get; set; }
        public string Path { get; set; }
        public bool ActiveFlag { get; set; }
        public List<ScreenNameDo> ScreenNames { get; set; }
        public List<ScreenPermissionDo> ScreenPermissions { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ScreenDo()
        {
            this.ScreenNames = new List<ScreenNameDo>();
            this.ScreenPermissions = new List<ScreenPermissionDo>();
        }
    }
    public class ScreenNameDo
    {
        public string AppCode { get; set; }
        public string ScreenId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
    }
    public class ScreenPermissionDo
    {
        public string AppCode { get; set; }
        public string ScreenId { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public bool HasPermission { get; set; }
    }

    public class ScreenUpdateDo
    {
        public string AppCode { get; set; }
        public string ScreenId { get; set; }
        public string ImageIcon { get; set; }
        public string Path { get; set; }
        public List<ScreenNameDo> ScreenNames { get; set; }
        public List<ScreenPermissionDo> ScreenPermissions { get; set; }
        public bool ActiveFlag { get; set; }
        public DateTime? LatestUpdateDate { get; set; }

        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
        public string Language { get; set; }

        public ScreenUpdateDo()
        {
            this.ScreenNames = new List<ScreenNameDo>();
            this.ScreenPermissions = new List<ScreenPermissionDo>();
        }
    }
    

    public class ScreenUpdateResultDo : AUpdateTransactionDo<ScreenDo>
    {
    }
}
