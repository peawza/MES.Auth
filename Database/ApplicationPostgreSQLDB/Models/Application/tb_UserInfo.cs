using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    [Table(name: "tb_UserInfo", Schema = "public")]
    [PrimaryKey(nameof(Id))]
    public class tb_UserInfo
    {
        [Column(TypeName = "VARCHAR(450)")]
        [Comment("User ID (Main Key)")]
        public string Id { get; set; }

        [Required]
        public int UserNumber { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(256)")]
        [Comment("UserName")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        [Comment("First Name")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        [Comment("Last Name")]
        public string LastName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Comment("Remark, more note.")]
        public string? Remark { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Comment("Language Code login")]
        public string? LanguageCode { get; set; }


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
    }
}
