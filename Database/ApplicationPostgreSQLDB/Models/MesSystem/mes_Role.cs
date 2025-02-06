#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.MesSystem
{
    [Table("mes_Role", Schema = "mes")]
    public partial class mes_Role
    {
        //public tb_Role()
        //{
        //    UserRoles = new HashSet<UserRole>();
        //}

        [Key] // Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string NormalizedName { get; set; }

        [MaxLength(50)]
        public string ConcurrencyStamp { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [MaxLength(50)]
        public string CreateBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; } // Nullable for optional updates

        //public virtual ICollection<UserRole> UserRoles { get; set; }
    }


    public partial class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }


}
