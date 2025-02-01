using Utils.Interfaces;

namespace Authentication.Models
{
    public class UpdateRoleDo : RoleDo
    {
        public string Language { get; set; }
    }

    public class UpdateRoleResultDo : AUpdateTransactionDo<RoleInfoDo>
    {
    }
}
