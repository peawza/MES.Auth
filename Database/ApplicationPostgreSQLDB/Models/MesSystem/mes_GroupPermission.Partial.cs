#nullable disable

namespace Application.Models.MesSystem
{
    public partial class mes_GroupPermission
    {

    }

    public partial class GroupPermissionDataView
    {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int Seq { get; set; }
        public string ScreenId { get; set; }
        public string ScreenName { get; set; }
        public int ScreenFunctionCode { get; set; }
        public int PermissionFunctionCode { get; set; }
    }
}
