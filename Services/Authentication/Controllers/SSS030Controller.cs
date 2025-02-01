using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Authentication.Services;
using Utils.Extensions;
using Authentication.Models;
using Application.Models;
using Authentication.Constants;
using Utils.Services;

namespace Authentication.Controllers
{
    [Authorize]
    [Route("api/v1/auth/[controller]")]
    public class SSS030Controller : ControllerBase
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationRoleManager roleManager;
        private readonly ApplicationSignInManager signInManager;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        private readonly ISSS030Service service;
        private readonly ILogger<CommonController> logger;

        public SSS030Controller(
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            ApplicationSignInManager signInManager,
            IConfiguration configuration,
            IEmailService emailService,
            ISSS030Service service,
            ILogger<CommonController> logger
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.service = service;
            this.configuration = configuration;
            this.emailService = emailService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("SearchUser")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> SearchUser([FromBody] UserSearchCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.SearchUser(oCriteria)));
        }
        [HttpPost]
        [Route("GetUser")]
        [Authorize(Policy = "SSS031-OpenUser")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> GetUser([FromBody] UserCriteriaDo oCriteria)
        {
            return Ok(await Task.FromResult(this.service.GetUser(oCriteria)));
        }
        [HttpPost]
        [Route("AddUser")]
        [Authorize(Policy = "SSS031-AddUser")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> AddUser([FromBody] UpdateUserDo oUser)
        {
            UpdateUserResultDo result = new UpdateUserResultDo();

            result.AddError(this.service.ValidateUser(oUser));
            if (result.HasError == false)
            {
                oUser.CreateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
                oUser.CreateBy = ClaimHelper.GetUserNumber(User.Claims);

                if (await this.userManager.CreateAsync(oUser))
                {
                    ApplicationUser appUser = await this.userManager.FindByNameAsync(oUser.UserName);
                    if (oUser.Password == null)
                    {
                        string token = await this.userManager.GeneratePasswordResetTokenAsync(appUser);

                        string templatePath = System.IO.Path.Combine("Templates", "Template.NewUser.html");
                        if (System.IO.File.Exists(templatePath))
                        {
                            string url = string.Format("{0}?i={1}&t={2}", AUTH.RESET_PASSWORD_URL, appUser.Id, System.Web.HttpUtility.UrlEncode(token));

                            string contentHTML = System.IO.File.ReadAllText(templatePath);
                            contentHTML = contentHTML.Replace("{Username}", appUser.UserName);
                            contentHTML = contentHTML.Replace("{Url}", url);

                            await this.emailService.SendAsync(new Utils.Models.EmailMessageDo()
                            {
                                Subject = "New Password",
                                To = appUser.Email,
                                Body = contentHTML
                            });
                        }
                    }

                    result.Data = this.service.GetUser(new UserCriteriaDo()
                    {
                        AppCode = oUser.AppCode,
                        UserId = appUser.Id
                    });
                }
                else
                    result.AddError("ES005");
            }

            return Ok(result);
        }
        [HttpPost]
        [Route("UpdateUser")]
        [Authorize(Policy = "SSS031-UpdateUser")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDo oUser)
        {
            UpdateUserResultDo result = new UpdateUserResultDo();

            result.AddError(this.service.ValidateUser(oUser));

            if (result.HasError == false)
            {
                oUser.UpdateDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
                oUser.UpdateBy = ClaimHelper.GetUserNumber(User.Claims);
       
                
                oldRoleDo? oldRole   =    await this.userManager.UpdateAsync(oUser, true);
                result.Data = this.service.GetUser(new UserCriteriaDo()
                {
                    AppCode = oUser.AppCode,
                    UserId = oUser.Id
                });



                result.Data.OldRoles = oldRole;


            }

            return Ok(result);
        }
        [HttpPost]
        [Route("DeleteUser")]
        [Authorize(Policy = "SSS031-DeleteUser")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> DeleteUser([FromBody] UpdateUserDo oUser)
        {
            UpdateUserResultDo result = new UpdateUserResultDo();

            await this.userManager.DeleteAsync(oUser);

            return Ok(result);
        }
    }
}