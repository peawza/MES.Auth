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
    [Table(name: "tb_UserInfo", Schema = "dbo")]
    [PrimaryKey(nameof(Id))]
    public class tb_UserInfo
    {
        [Column(TypeName = "NVARCHAR(450)")]
        [Comment("User ID (Main Key)")]
        public string Id { get; set; }

        [Required]
        public int UserNumber { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        [Comment("UserName")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        [Comment("First Name")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        [Comment("Last Name")]
        public string LastName { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        [Comment("Remark, more note.")]
        public string? Remark { get; set; }

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
