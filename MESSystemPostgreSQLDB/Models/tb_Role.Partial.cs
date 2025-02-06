namespace Master.Libs.Core.Models
{
    public class RoleView
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public string? UpdateDisplayName { get; set; }


        public string? CreateDisplayName { get; set; }
    }
}
