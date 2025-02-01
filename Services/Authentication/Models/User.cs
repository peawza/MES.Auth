using Utils.Interfaces;

namespace Authentication.Models
{
    public class UserCriteriaDo
    {
        public string AppCode { get; set; }
        public string UserId { get; set; }
    }
    public class UserDo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public bool ActiveFlag { get; set; }

        public DateTime? UpdateDate { get; set; }
        public string UpdateByName { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? InActiveDate { get; set; }

        public List<UserRoleDo> Roles { get; set; }

        public UserDo()
        {
            this.Roles = new List<UserRoleDo>();
        }

        public oldRoleDo? OldRoles { get; set; } 

    }
    public class UserRoleDo
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
    }

    public class UpdateUserResultDo : AUpdateTransactionDo<UserDo>
    {
    }
    public class UpdateUserDo
    {
        public string AppCode { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public bool ActiveFlag { get; set; }

        public List<UpdateUserRole> Roles { get; set; }

        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }

        public UpdateUserDo()
        {
            this.Roles = new List<UpdateUserRole>();
        }
    }
    public class UpdateUserRole
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
    }

    public class UserInfoCriteriaDo
    {
        public string Name { get; set; }
    }
    public class UserInfoDo
    {
        public int UserNumber { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string NameWithCode { get; set; }
    }

    public class UserSearchCriteriaDo : ASearchCriteriaDo
    {
        public string AppCode { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public List<string> RoleIds { get; set; }
        public bool? ActiveFlag { get; set; }

        public UserSearchCriteriaDo()
        {
            this.RoleIds = new List<string>();
        }
    }
    public class UserSearchResultDo : ASearchResultDo<UserSearchDo>
    {
    }

    public class UserSearchDo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool ActiveFlag { get; set; }

        public List<UserRoleSearchDo> Roles { get; set; }

        public UserSearchDo()
        {
            this.Roles = new List<UserRoleSearchDo>();
        }
    }
    public class UserRoleSearchDo
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class oldRoleDo
    {
        public List<string> addRole { get; set; }
        public List<string> delRole { get; set; }
    }
}
