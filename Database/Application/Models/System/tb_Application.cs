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
    [Table(name: "tb_Application", Schema = "dbo")]
    [PrimaryKey(nameof(AppCode))]
    public class tb_Application
    {
        [Column(TypeName = "NVARCHAR(10)")]
        public string AppCode { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        public string AppName { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        [Required]
        [Column(TypeName = "DATETIME")]
        [Comment("Create Date")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Comment("Create By")]
        public int CreateBy { get; set; }
    }
}
