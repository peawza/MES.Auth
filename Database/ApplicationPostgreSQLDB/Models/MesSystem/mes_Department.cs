#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.MesSystem
{
    [Table("mes_Department", Schema = "mes")]
    public partial class mes_Department
    {
        [Key] // Defines this as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment for integer primary key
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string DepartmentNo { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; }
    }
}
