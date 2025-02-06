using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class MESScreenModel
    {

        #region LocalizedMessages
        public class MESScreenCriteria
        {
            [Required]
            public string? SupportDeviceType { get; set; }
        }

        public class MESScreenResult
        {
            public string ScreenId { get; set; }
            public string Name_EN { get; set; }
            public string Name_TH { get; set; }
            public int FunctionCode { get; set; }
            public string ModuleCode { get; set; }
            public string ModuleName_EN { get; set; }
            public string ModuleName_TH { get; set; }
            public int ModuleName_Seq { get; set; }
            public string? ModuleName_IconClass { get; set; }
            public string SubModuleCode { get; set; }
            public string SubModuleName_EN { get; set; }
            public string SubModuleName_TH { get; set; }
            public string SubModule_IconClass { get; set; }
            public string Screen_IconClass { get; set; }
            public int? Screen_MainMenuFlag { get; set; }
            public int? Screen_PermissionFlag { get; set; }
            public int Screen_Seq { get; set; }





        }
        #endregion
    }
}
