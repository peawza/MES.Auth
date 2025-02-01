using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Authentication.Services;
using System.Data;
using Authentication.Constants;
using Utils.Extensions;
using Authentication.Models;

namespace Authentication.Controllers
{
    [Authorize]
    [Route("api/v1/auth/[controller]")]
    public class CommonController : ControllerBase
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationRoleManager roleManager;
        private readonly ApplicationSignInManager signInManager;
        private readonly IConfiguration configuration;
        private readonly ISystemService service;
        private readonly ILogger<CommonController> logger;
        
        public CommonController(
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            ApplicationSignInManager signInManager,
            IConfiguration configuration,
            ISystemService service,
            ILogger<CommonController> logger
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.service = service;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Logout")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> Logout([FromBody] Models.RefreshTokenDo oToken)
        {
            await this.signInManager.SignOutAsync();
            return Ok();
        }
        [HttpPost]
        [Route("IsAuthenticated")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> IsAuthenticated()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated == true)
                return Ok(true);

            return Unauthorized();
        }
        [HttpPost]
        [Route("IsUserInRole")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> IsUserInRole([FromBody] UserInRoleDo oUserInRole)
        {
            List<UserInRolePermissionDo> permissions = new List<UserInRolePermissionDo>();

            foreach (UserInRolePermissionDo pm in oUserInRole.Permissions)
            {
                var claim = User.Claims.Where(x =>
                {
                    return x.Type == ROLE.CLAIM_TYPE_PERMISSION
                            && x.Value == $"{oUserInRole.AppCode}.{pm.ScreenId}.{pm.PermissionCode}";
                }).FirstOrDefault();
                if (claim != null)
                    permissions.Add(pm);
            }

            return Ok(permissions);
        }
        [HttpPost]
        [Route("GetUsers")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetUsers([FromBody] UserInfoCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.GetUsers(oCriteria)));
        }
        [HttpPost]
        [Route("GetRoles")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetRoles([FromBody] RoleCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.GetRoles(oCriteria)));
        }
        [HttpPost]
        [Route("GetScreenInfo")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetScreenInfo([FromBody] ScreenInfoCriteriaDo oCriteria)
        {
            var claims = User.Claims.Where(x =>
            {
                return x.Type == ROLE.CLAIM_TYPE_PERMISSION;
            }).Select(x => x.Value);
            if (claims != null)
            {
                foreach (var claim in claims)
                {
                    string[] ps = claim.Split(".".ToCharArray());
                    if (ps.Length == 3)
                    {
                        if (ps[0] == oCriteria.AppCode && ps[2] == ROLE.PERMISSION_OPEN) //Has Permission Open
                            oCriteria.Screens.Add(ps[1]);
                    }
                }
            }

            return Ok(await Task.FromResult(this.service.GetScreenInfo(oCriteria)));
        }
    }
}