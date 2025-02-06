using System;
using System.Collections.Generic;

#nullable disable

namespace Master.Libs.Core.Models
{
    public partial class tb_Screen
    {
        public string ScreenId { get; set; }
        public string Name { get; set; }
        public int FunctionCode { get; set; }
        public string ModuleCode { get; set; }
        public string IconClass { get; set; }
        public int? Attributes { get; set; }
        public int Seq { get; set; }
    }
}
