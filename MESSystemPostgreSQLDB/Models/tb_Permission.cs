namespace Master.Libs.Core.Models
{
    public partial class tb_Permission
    {
        public Guid Id { get; set; }
        public int GroupId { get; set; }
        public string ScreenId { get; set; }
        public int FunctionCode { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }

    public partial class PermissionDataView
    {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int Seq { get; set; }
        public string ScreenId { get; set; }
        public string ScreenName { get; set; }
        public int ScreenFunctionCode { get; set; }
        public int PermissionFunctionCode { get; set; }
    }
}
