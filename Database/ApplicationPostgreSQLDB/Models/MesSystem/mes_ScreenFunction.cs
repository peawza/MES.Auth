#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.MesSystem
{
    [Table("mes_ScreenFunction", Schema = "mes")]
    public partial class mes_ScreenFunction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FunctionId { get; set; }

        [Required]
        public int FunctionCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string FunctionName { get; set; }
    }
}
