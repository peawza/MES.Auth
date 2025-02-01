namespace Authentication.Models
{
    public class UserInRoleDo
    {
        public string AppCode { get; set; }
        public List<UserInRolePermissionDo> Permissions { get; set; }

        public UserInRoleDo()
        {
            this.Permissions = new List<UserInRolePermissionDo>();
        }
    }
    public class UserInRolePermissionDo
    {
        public string ScreenId { get; set; }
        public string PermissionCode { get; set; }
    }
}
