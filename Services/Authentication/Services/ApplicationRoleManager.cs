using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Authentication.Constants;
using Application.Models;
using Authentication.Models;

namespace Authentication.Services
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store,
                                        IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
                                        ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
                                        ILogger<RoleManager<ApplicationRole>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        public async Task<bool> CreateAsync(RoleDo oRole)
        {
            string name = $"{oRole.AppCode}.{oRole.RoleName}";
            ApplicationRole appRole = await this.FindByNameAsync(name);
            if (appRole != null)
                return false;

            ApplicationRole newRole = new ApplicationRole()
            {
                Name = name,
                AppCode = oRole.AppCode,
                RoleName = oRole.RoleName,
                Description = oRole.Description,
                ActiveFlag = oRole.ActiveFlag,
                CreateDate = oRole.CreateDate.Value,
                CreateBy = oRole.CreateBy.Value,
                UpdateDate = oRole.CreateDate.Value,
                UpdateBy = oRole.CreateBy.Value
            };

            IdentityResult res = await this.CreateAsync(newRole);
            if (res.Succeeded == true)
            {
                appRole = await this.FindByNameAsync(name);

                if (oRole.Permissions.Count > 0)
                {
                    foreach(var permission in oRole.Permissions)
                    {
                        await this.AddClaimAsync(appRole, 
                            new Claim(ROLE.CLAIM_TYPE_PERMISSION, $"{appRole.AppCode}.{permission.ScreenId}.{permission.PermissionCode}"));
                    }
                }
            }

            return true;
        }
        public async Task<bool> UpdateAsync(RoleDo oRole)
        {
            string name = $"{oRole.AppCode}.{oRole.RoleName}";

            ApplicationRole appRole = await this.FindByIdAsync(oRole.RoleId);
            if (appRole != null)
            {
                appRole.Name = name;
                appRole.AppCode = oRole.AppCode;
                appRole.RoleName = oRole.RoleName;
                appRole.Description = oRole.Description;
                appRole.ActiveFlag = oRole.ActiveFlag;
                appRole.UpdateDate = oRole.UpdateDate.Value;
                appRole.UpdateBy = oRole.UpdateBy.Value;

                IdentityResult res = await this.UpdateAsync(appRole);
                if (res.Succeeded == true)
                {
                    foreach(var claim in await this.GetClaimsAsync(appRole))
                    {
                        await this.RemoveClaimAsync(appRole, claim);
                    }

                    if (oRole.Permissions.Count > 0)
                    {
                        foreach (var permission in oRole.Permissions)
                        {
                            await this.AddClaimAsync(appRole,
                                new Claim(ROLE.CLAIM_TYPE_PERMISSION, $"{appRole.AppCode}.{permission.ScreenId}.{permission.PermissionCode}"));
                        }
                    }
                }

                return true;
            }

            return false;
        }
        public async Task<bool> DeleteAsync(RoleDo oRole)
        {
            ApplicationRole appRole = await this.FindByIdAsync(oRole.RoleId);
            if (appRole != null)
            {
                await this.DeleteAsync(appRole);
                
                return true;
            }

            return false;
        }
    }
}
