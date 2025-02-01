using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public bool FirstLoginFlag { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        [Required]
        public bool SystemAdminFlag { get; set; }

        public int? PasswordAge { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime? LastLoginDate { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime? LastUpdatePasswordDate { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime? ActiveDate { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime? InActiveDate { get; set; }

        public virtual ICollection<tb_PasswordHistory> PasswordHistories { get; set; }

        public ApplicationUser()
        {
            this.PasswordHistories = new HashSet<tb_PasswordHistory>();
        }
    }
}
