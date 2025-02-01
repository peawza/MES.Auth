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
    [Table(name: "tb_PasswordHistory", Schema = "dbo")]
    [PrimaryKey(nameof(Id), nameof(HistoryId))]
    public class tb_PasswordHistory
    {
        [Column(TypeName = "NVARCHAR(450)")]
        [Comment("User ID (Main Key)")]
        public string Id { get; set; }

        public int HistoryId { get; set; }

        [Required]
        [Column(TypeName = "DATETIME")]
        public DateTime HistoryDate { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string PasswordHash { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
