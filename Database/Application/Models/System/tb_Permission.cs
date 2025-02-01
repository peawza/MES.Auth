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
    [Table(name: "tb_Permission", Schema = "dbo")]
    [PrimaryKey(nameof(PermissionCode))]
    public class tb_Permission
    {
        [Column(TypeName = "NVARCHAR(10)")]
        [Comment("Permission Code")]
        public string PermissionCode { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        [Comment("Permission Description")]
        public string? Description { get; set; }

        [Required]
        [Comment("Sequence No. for sorting display list.")]
        public int SeqNo { get; set; }

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

        public virtual ICollection<tb_PermissionName> PermissionNames { get; set; }

        public virtual ICollection<tb_ScreenPermission> ScreenPermissions { get; set; }

        public tb_Permission()
        {
            this.PermissionNames = new HashSet<tb_PermissionName>();
            this.ScreenPermissions = new HashSet<tb_ScreenPermission>();
        }
    }
}
