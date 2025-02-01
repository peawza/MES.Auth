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
    [Table(name: "tb_PasswordHistory", Schema = "public")]
    [PrimaryKey(nameof(Id), nameof(HistoryId))]
    public class tb_PasswordHistory
    {
        [Column(TypeName = "VARCHAR(450)")]
        [Comment("User ID (Main Key)")]
        public string Id { get; set; }

        public int HistoryId { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime HistoryDate { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        public string PasswordHash { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
