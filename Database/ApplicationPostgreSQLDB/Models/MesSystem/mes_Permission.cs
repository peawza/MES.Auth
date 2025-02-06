using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.MesSystem
{
    [Table("mes_Permission", Schema = "mes")]
    public partial class mes_Permission
    {
        [Key] // Primary key for the table
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public int GroupId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ScreenId { get; set; }

        [Required]
        public int FunctionCode { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        [MaxLength(50)]
        public string? UpdateBy { get; set; }
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
