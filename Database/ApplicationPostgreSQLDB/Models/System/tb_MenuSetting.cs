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
    [Table(name: "tb_MenuSetting", Schema = "public")]
    [PrimaryKey(nameof(AppCode), nameof(MenuId))]
    public class tb_MenuSetting
    {
        [Column(TypeName = "VARCHAR(10)")]
        public string AppCode { get; set; }

        [Comment("Running Unique Record Id")]
        public int MenuId { get; set; }

        [Required]
        [Column(TypeName = "CHAR(1)")]
        [Comment("G = Group Folder, I = Item")]
        public string MenuType { get; set; }

        [Comment("Parent Menu Id")]
        public int? ParentMenuId { get; set; }

        [Required]
        [Comment("Sequence No. (Re-Order every updating)")]
        public int SeqNo { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [Comment("Screen Icon Name")]
        public string? ImageIcon { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        [Comment("Screen Id")]
        public string? ScreenId { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        [Comment("Link")]
        public string? MenuURL { get; set; }

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

        public virtual tb_Screen Screen { get; set; }
        public virtual ICollection<tb_MenuName> MenuNames { get; set; }

        public tb_MenuSetting()
        {
            this.MenuNames = new HashSet<tb_MenuName>();
        }
    }
}
