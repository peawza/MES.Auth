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
    [Table(name: "tb_Screen", Schema = "dbo")]
    [PrimaryKey(nameof(AppCode), nameof(ScreenId))]
    public class tb_Screen
    {
        [Column(TypeName = "NVARCHAR(10)")]
        public string AppCode { get; set; }

        [Column(TypeName = "NVARCHAR(10)")]
        [Comment("Screen Id")]
        public string ScreenId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        [Comment("Screen Icon Name")]
        public string ImageIcon { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        [Comment("Screen Path")]
        public string Path { get; set; }

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

        public virtual ICollection<tb_ScreenName> ScreenNames { get; set; }

        public virtual ICollection<tb_ScreenPermission> ScreenPermissions { get; set; }

        public virtual ICollection<tb_MenuSetting> MenuSettings { get; set; }

        public tb_Screen()
        {
            this.ScreenNames = new HashSet<tb_ScreenName>();
            this.ScreenPermissions = new HashSet<tb_ScreenPermission>();
            this.MenuSettings = new HashSet<tb_MenuSetting>();
        }
    }
}
