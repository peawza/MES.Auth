using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ApplicationRole : IdentityRole
    {
        [Required]
        [Column(TypeName = "NVARCHAR(10)")]
        public string AppCode { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        public string RoleName { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Description { get; set; }

        [Required]
        public bool SystemAdminFlag { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        [Required]
        [Column(TypeName = "DATETIME")]
        [Comment("Create Date")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Comment("Create By")]
        public int CreateBy { get; set; }

        [Required]
        [Column(TypeName = "DATETIME")]
        [Comment("Update Date")]
        public DateTime UpdateDate { get; set; }

        [Required]
        [Comment("Update By")]
        public int UpdateBy { get; set; }
    }
}
