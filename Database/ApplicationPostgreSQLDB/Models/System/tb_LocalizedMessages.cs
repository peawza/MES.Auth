using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{


    [Table(name: "tb_LocalizedMessages", Schema = "public")]
    [PrimaryKey(nameof(MessageCode), nameof(MessageType))]
    public class tb_LocalizedMessages
    {
        [Key]
        [Column(TypeName = "VARCHAR(50)")]
        public string MessageCode { get; set; }
        [Key]

        [Column(TypeName = "VARCHAR(20)")]
        public string MessageType { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? MessageNameEN { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? MessageNameTH { get; set; }

        [Column(TypeName = "VARCHAR(256)")]
        public string? Remark { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime? CreateDate { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string? CreateBy { get; set; }
    }



}
