#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.MesSystem
{
    [Table("mes_GroupPermission", Schema = "mes")]
    public partial class mes_GroupPermission
    {
        [Key] // Primary key for the table
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; } // Assuming this is a foreign key

        [Required]
        [MaxLength(50)]
        public string ScreenId { get; set; } // Ensure it has a defined length

        [Required]
        public int FunctionCode { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow; // Default to current time

        [Required]
        [MaxLength(50)]
        public string CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; } // Nullable to allow updates

        [MaxLength(50)]
        public string UpdateBy { get; set; }
    }
}
