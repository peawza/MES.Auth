#nullable disable

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.MesSystem
{
    [Table("mes_UserRoles", Schema = "mes")]
    public partial class mes_UserRoles
    {


        [Key] // Primary key
        [Column(TypeName = "VARCHAR(450)")]
        [Comment("User ID (Main Key)")]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public int RoleId { get; set; }


    }




}
