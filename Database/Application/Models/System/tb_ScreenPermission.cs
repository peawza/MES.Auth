using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    [Table(name: "tb_ScreenPermission", Schema = "dbo")]
    [PrimaryKey(nameof(AppCode), nameof(ScreenId), nameof(PermissionCode))]
    public class tb_ScreenPermission
    {
        [Column(TypeName = "NVARCHAR(10)")]
        public string AppCode { get; set; }

        [Column(TypeName = "NVARCHAR(10)")]
        [Comment("Screen Id")]
        public string ScreenId { get; set; }

        [Column(TypeName = "NVARCHAR(10)")]
        [Comment("Permission Code")]
        public string PermissionCode { get; set; }

        public virtual tb_Screen Screen { get; set; }
        public virtual tb_Permission Permission { get; set; }
    }
}
