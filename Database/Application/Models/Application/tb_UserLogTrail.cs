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
    [Table(name: "tb_UserLogTrail", Schema = "dbo")]
    public class tb_UserLogTrail
    {
       

        [Required]
        [Column(TypeName = "Guid")]
        [Comment("UserID")]
        public Guid user_id { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        [Comment("Create Date")]
        public string create_date { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        [Comment("Create User")]
        public string create_user { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        [Comment("Action Name")]
        public string action_name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        [Comment("Data Before")]
        public string data_before { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        [Comment("Data After")]
        public string data_after { get; set; }
    }
}
