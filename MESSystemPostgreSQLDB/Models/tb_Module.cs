namespace Master.Libs.Core.Models
{
    public partial class tb_Module
    {
        public string? ModuleCode { get; set; }
        public string? ModuleName { get; set; }
        public int Seq { get; set; }
        public string? IconClass { get; set; }
    }
    public partial class DefectModule
    {
        public int Id { get; set; }
        public string? DefectGroup { get; set; }
        public string? Module { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public Nullable<int> Seq { get; set; }
    }
}
