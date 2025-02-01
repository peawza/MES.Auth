using Application.Models;
using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.Extensions;

namespace Authentication.Controllers
{
    [Authorize]
    [Route("api/v1/auth/[controller]")]
    public class SSS040Controller : ControllerBase
    {
        private readonly ApplicationRoleManager roleManager;
        private readonly ISSS040Service service;
        private readonly ISystemService systemService;
        private readonly ILogger<SSS040Controller> logger;

        public SSS040Controller(
             ApplicationRoleManager roleManager,
            ISSS040Service service,
            ISystemService systemService,
            ILogger<SSS040Controller> logger
        )
        {
            this.roleManager = roleManager;
            this.service = service;
            this.systemService = systemService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("SearchRole")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> SearchRole([FromBody] RoleSearchCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.SearchRole(oCriteria)));
        }
        [HttpPost]
        [Route("GetRole")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetRole([FromBody] RoleCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.GetRole(oCriteria)));
        }
        [HttpPost]
        [Route("AddRole")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> AddRole([FromBody] UpdateRoleDo oRole)
        {
            UpdateRoleResultDo result = new UpdateRoleResultDo();

            result.AddError(this.service.ValidateRole(oRole));
            if (result.HasError == false)
            {
                oRole.CreateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
                oRole.CreateBy = ClaimHelper.GetUserNumber(User.Claims);

                bool succeeded = await this.roleManager.CreateAsync(oRole);
                if (succeeded)
                {
                    ApplicationRole? role = await this.roleManager.FindByNameAsync($"{oRole.AppCode}.{oRole.RoleName}");
                    result.Data = this.service.GetRole(new RoleCriteriaDo()
                    {
                        RoleId = role.Id,
                        AppCode = role.AppCode,
                        Language = oRole.Language
                    });
                }
                else
                {
                    result.AddError("E0013;" + oRole.RoleName);
                }
            }

            return Ok(await Task.FromResult(result));
        }
        [HttpPost]
        [Route("UpdateRole")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDo oRole)
        {
            UpdateRoleResultDo result = new UpdateRoleResultDo();

            result.AddError(this.service.ValidateRole(oRole));
            if (result.HasError == false)
            {
                oRole.UpdateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
                oRole.UpdateBy = ClaimHelper.GetUserNumber(User.Claims);

                await this.roleManager.UpdateAsync(oRole);
                result.Data = this.service.GetRole(new RoleCriteriaDo()
                {
                    RoleId = oRole.RoleId,
                    AppCode = oRole.AppCode,
                    Language = oRole.Language
                });
            }

            return Ok(await Task.FromResult(result));
        }
        [HttpPost]
        [Route("DeleteRole")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> DeleteRole([FromBody] RoleDo oRole)
        {
            UpdateRoleResultDo result = new UpdateRoleResultDo();

            result.AddError(this.service.ValidateRoleForDelete(oRole));
            if (result.HasError == false)
            {
                await this.roleManager.DeleteAsync(oRole);
            }

            return Ok(await Task.FromResult(result));
        }
        [HttpPost]
        [Route("SearchUserInRole")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> SearchUserInRole([FromBody] UserInRoleSearchCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.SearchUserInRole(oCriteria)));
        }
        [HttpPost]
        [Route("GetScreensAllowList")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetScreens([FromBody] RoleCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.GetScreensAllowList(oCriteria)));
        }
    }
}