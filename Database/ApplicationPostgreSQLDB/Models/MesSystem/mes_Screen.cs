#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.MesSystem
{
    [Table("mes_Screen", Schema = "mes")]
    public partial class mes_Screen
    {
        [Key]
        [MaxLength(50)]
        public string ScreenId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name_EN { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name_TH { get; set; }

        [MaxLength(100)]
        public string SupportDeviceType { get; set; }

        [Required]
        public int FunctionCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string ModuleCode { get; set; }


        [MaxLength(50)]
        public string? SubModuleCode { get; set; }

        [MaxLength(50)]
        public string IconClass { get; set; }


        public int MainMenuFlag { get; set; }

        public int PermissionFlag { get; set; }

        [Required]
        public int Seq { get; set; }
    }
}
