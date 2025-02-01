using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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

        [Column(TypeName = "DATETIME")]
        public DateTime? LastLoginDate { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? LastUpdatePasswordDate { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? ActiveDate { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? InActiveDate { get; set; }

        public virtual ICollection<tb_PasswordHistory> PasswordHistories { get; set; }

        public ApplicationUser()
        {
            this.PasswordHistories = new HashSet<tb_PasswordHistory>();
        }
    }
}
