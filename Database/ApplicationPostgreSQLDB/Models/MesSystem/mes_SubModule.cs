using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.MesSystem
{
    [Table("mes_SubModule", Schema = "mes")]
    public partial class mes_SubModule
    {
        [Key] // Primary key for EF Core
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string SubModuleCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubModuleName_EN { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubModuleName_TH { get; set; }

        [Required]
        public int Seq { get; set; }

        [MaxLength(50)]
        public string? IconClass { get; set; }
    }
    public partial class DefectSubModule
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
