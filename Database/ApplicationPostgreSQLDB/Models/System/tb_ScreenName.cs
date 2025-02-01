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
    [Table(name: "tb_ScreenName", Schema = "public")]
    [PrimaryKey(nameof(AppCode), nameof(ScreenId), nameof(Language))]
    public class tb_ScreenName
    {
        [Column(TypeName = "VARCHAR(10)")]
        public string AppCode { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        [Comment("Screen Id")]
        public string ScreenId { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        [Comment("Language Code such as en-US, th-TH")]
        public string Language { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        [Comment("Menu Name of Language")]
        public string Name { get; set; }

        public virtual tb_Screen Screen { get; set; }
    }
}
