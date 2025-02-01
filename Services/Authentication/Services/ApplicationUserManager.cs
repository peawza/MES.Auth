using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Authentication.Repositories;
using Application.Models;
using Authentication.Models;
using System.Collections.Generic;

namespace Authentication.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly IApplicationRepository? repository;

        public ApplicationUserManager(IUserStore<ApplicationUser> store,
                                        IOptions<IdentityOptions> optionsAccessor,
                                        IPasswordHasher<ApplicationUser> passwordHasher,
                                        IEnumerable<IUserValidator<ApplicationUser>> userValidators,
                                        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
                                        ILookupNormalizer keyNormalizer,
                                        IdentityErrorDescriber errors,
                                        IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.repository = services.GetService<IApplicationRepository>();
        }

        public async Task<bool> CreateAsync(UpdateUserDo user)
        {
            try
            {
                ApplicationUser? appUser = await this.FindByNameAsync(user.UserName);
                if (appUser != null)
                    return false;

                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = user.UserName,
                    FirstLoginFlag = false,
                    ActiveFlag = user.ActiveFlag,
                    PasswordAge = null,
                    LastUpdatePasswordDate = user.CreateDate,
                    ActiveDate = (user.ActiveFlag == true) ? user.CreateDate : null,
                    InActiveDate = !(user.ActiveFlag == true) ? user.CreateDate : null,
                    Email = user.Email,
                    EmailConfirmed = true,
                    TwoFactorEnabled = true
                };

                IdentityResult res;
                if (user.Password == null)
                    res = await this.CreateAsync(newUser);
                else
                    res = await this.CreateAsync(newUser, user.Password);
                if (res.Succeeded == true)
                {
                    appUser = await this.FindByNameAsync(user.UserName);

                    this.repository.AddUserInfo(appUser, user);

                    if (user.Roles.Count > 0)
                    {
                        string[] roles = user.Roles
                            .Select(x => string.Format("{0}.{1}", user.AppCode, x.RoleName))
                            .ToArray();
                        res = await this.AddToRolesAsync(appUser, roles);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<oldRoleDo> UpdateAsync(UpdateUserDo user, bool updateRole)
        {
            oldRoleDo oldRole = new oldRoleDo();
            ApplicationUser? appUser = await this.FindByIdAsync(user.Id);
            if (appUser == null)
                return null;

            if (appUser.ActiveFlag != user.ActiveFlag)
            {
                appUser.ActiveDate = (user.ActiveFlag == true) ? user.UpdateDate : null;
                appUser.InActiveDate = !(user.ActiveFlag == true) ? user.UpdateDate : null;
            }

            appUser.ActiveFlag = user.ActiveFlag;
            appUser.Email = user.Email;

            if (updateRole == true)
            {
         
                List<string> addRoles = new List<string>();
                List<string> delRoles = new List<string>();
                oldRole.addRole= new List<string>();
                oldRole.delRole = new List<string>();


                var roles = await this.GetRolesAsync(appUser);
                foreach (var role in roles)
                {
                    if (user.Roles.Where(x => x.Name == role).Count() == 0) {
                        delRoles.Add(role);
                        oldRole.delRole.Add(role);
                    }
                }
                foreach (var role in user.Roles)
                {
                    if (roles.Where(x => x == role.Name).Count() == 0) {
                        addRoles.Add(role.Name);
                        oldRole.addRole.Add(role.RoleName);
                    }
                }

                await this.RemoveFromRolesAsync(appUser, delRoles.ToArray());
                await this.AddToRolesAsync(appUser, addRoles.ToArray());
            }

            IdentityResult res = await this.UpdateAsync(appUser);
            if (res.Succeeded == true)
                this.repository.UpdateUserInfo(appUser, user);

            return oldRole;
        }
        public async Task<bool> DeleteAsync(UpdateUserDo user)
        {
            ApplicationUser appUser = await this.FindByIdAsync(user.Id);
            if (appUser == null)
                return false;

            IdentityResult res = await this.DeleteAsync(appUser);
            return res == IdentityResult.Success;
        }

        public async Task<string> GenerateRefreshTokenAsync(ApplicationUser appUser)
        {
            return await this.GenerateUserTokenAsync(appUser, "RefreshToken", "RefreshToken");
        }
        public async Task<bool> VerifyRefreshTokenAsync(ApplicationUser appUser, string token)
        {
            return await this.VerifyUserTokenAsync(appUser, "RefreshToken", "RefreshToken", token);
        }

        public UserInfoDo GetUserInfo(ApplicationUser user)
        {
            return this.repository.GetUserInfo(user);
        }
    }
}
