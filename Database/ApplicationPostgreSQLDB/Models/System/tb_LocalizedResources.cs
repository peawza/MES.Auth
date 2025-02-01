using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{

    [Table("tb_LocalizedResources", Schema = "public")]
    [PrimaryKey(nameof(ScreenCode), nameof(ObjectID))]
    public class tb_LocalizedResources
    {
        [Key]
        [Column(TypeName = "VARCHAR(20)")]
        public string ScreenCode { get; set; } = null!;

        [Key]
        [Column(TypeName = "VARCHAR(50)")]
        public string ObjectID { get; set; } = null!;

        [Column(TypeName = "VARCHAR(256)")]
        public string? ResourcesEN { get; set; }

        [Column(TypeName = "VARCHAR(256)")]
        public string? ResourcesTH { get; set; }

        [Column(TypeName = "VARCHAR(256)")]
        public string? Remark { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime? CreateDate { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string? CreateBy { get; set; }
    }
}
