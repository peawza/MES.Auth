using System;
using System.Collections.Generic;

#nullable disable

namespace Master.Libs.Core.Models
{
    public partial class tb_GroupPermission
    {
        public int GroupId { get; set; }
        public string ScreenId { get; set; }
        public int FunctionCode { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
