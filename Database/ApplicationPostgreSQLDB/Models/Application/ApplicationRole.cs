using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public class ApplicationRole : IdentityRole
    {
        [Required]
        [Column(TypeName = "VARCHAR(10)")]
        public string AppCode { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(256)")]
        public string RoleName { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string? Description { get; set; }

        [Required]
        public bool SystemAdminFlag { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        [Comment("Create Date")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Comment("Create By")]
        public int CreateBy { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        [Comment("Update Date")]
        public DateTime UpdateDate { get; set; }

        [Required]
        [Comment("Update By")]
        public int UpdateBy { get; set; }
    }
}
