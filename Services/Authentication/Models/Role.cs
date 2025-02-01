using Utils.Interfaces;

namespace Authentication.Models
{
    public class RoleCriteriaDo
    {
        public string RoleId { get; set; }
        public string AppCode { get; set; }
        public string Language { get; set; }
    }
    public class RoleDo
    {
        public string RoleId { get; set; }
        public string AppCode { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool ActiveFlag { get; set; }

        public List<RolePermissionDo> Permissions { get; set; }

        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public string CreateByName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
        public string UpdateByName { get; set; }

        public RoleDo()
        {
            this.Permissions = new List<RolePermissionDo>();
        }
    }
    public class RolePermissionDo
    {
        public int? Id { get; set; }
        public string ScreenId { get; set; }
        public string PermissionCode { get; set; }
    }

    public class RoleScreenDo
    {
        public string ScreenId { get; set; }
        public string ScreenName { get; set; }
        public string ImageIcon { get; set; }

        public List<RoleScreenPermissionDo> Permissions { get; set; }

        public RoleScreenDo()
        {
            this.Permissions = new List<RoleScreenPermissionDo>();
        }
    }
    public class RoleScreenPermissionDo
    {
        public string AppCode { get; set; }
        public string ScreenId { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public bool HasPermission { get; set; }
    }

    public class RoleClaimDo
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }


    public class RoleSearchCriteriaDo : ASearchCriteriaDo
    {
        public string AppCode { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
    }
    public class RoleSearchResultDo : ASearchResultDo<RoleSearchDo>
    {
    }

    public class RoleSearchDo
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int NoUser { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool ActiveFlag { get; set; }
    }

    public class UserInRoleSearchCriteriaDo : ASearchCriteriaDo
    {
        public string RoleId { get; set; }
        public string UserName { get; set; }
    }
    public class UserInRoleSearchResultDo : ASearchResultDo<UserInRoleSearchDo>
    {
    }
    public class UserInRoleSearchDo
    {
        public string UserName { get; set; }
        public string Name { get; set; }
    }
    public class RoleInfoDo
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool ActiveFlag { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateByName { get; set; }

        public List<RoleScreenDo> Screens { get; set; }

        public RoleInfoDo()
        {
            this.Screens = new List<RoleScreenDo>();
        }
    }
}
