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
    [Table(name: "tb_PermissionName", Schema = "public")]
    [PrimaryKey(nameof(PermissionCode), nameof(Language))]
    public class tb_PermissionName
    {
        [Column(TypeName = "VARCHAR(10)")]
        [Comment("Permission Code")]
        public string PermissionCode { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        [Comment("Language Code such as en-US, th-TH")]
        public string Language { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [Comment("Screen Name of Language")]
        public string Name { get; set; }

        public virtual tb_Permission Permission { get; set; }
    }
}
