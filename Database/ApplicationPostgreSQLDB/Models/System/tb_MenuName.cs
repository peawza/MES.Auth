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
    [Table(name: "tb_MenuName", Schema = "public")]
    [PrimaryKey(nameof(AppCode), nameof(MenuId), nameof(Language))]
    public class tb_MenuName
    {
        [Column(TypeName = "VARCHAR(10)")]
        public string AppCode { get; set; }

        [Comment("Running Unique Record Id")]
        public int MenuId { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        [Comment("Language Code such as en-US, th-TH")]
        public string Language { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [Comment("Menu Name of Language")]
        public string Name { get; set; }

        public virtual tb_MenuSetting MenuSetting { get; set; }
    }
}
