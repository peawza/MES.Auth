using Application.Models;
using Authentication.Constants;
using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Encodings.Web;
using Utils.Extensions;
using Utils.Services;

namespace Authentication.Controllers
{
    [Route("[controller]")]
    public class SSS010Controller : ControllerBase
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationRoleManager roleManager;
        private readonly ApplicationSignInManager signInManager;
        private readonly ISystemService service;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        private readonly UrlEncoder urlEncoder;
        private readonly ILogger<SSS010Controller> logger;

        public SSS010Controller(
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            ApplicationSignInManager signInManager,
            ISystemService service,
            IConfiguration configuration,
            IEmailService emailService,
            UrlEncoder urlEncoder,
            ILogger<SSS010Controller> logger
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.service = service;
            this.configuration = configuration;
            this.emailService = emailService;
            this.urlEncoder = urlEncoder;
            this.logger = logger;
        }

        [HttpGet]
        [Route("Initial")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> Initial(ApplicationDo oApp)
        {
            await this.signInManager.SignOutAsync();

            Models.UserLoginDo? user = null;

            #region Remember

            string serialize = "";
            if (HttpContext.Request.Cookies.TryGetValue(string.Format("{0}:{1}", oApp.AppCode, Constants.AUTH.REMEMBER_USER_NAME), out serialize))
            {
                Utils.Extensions.Encryption encrypt = new Utils.Extensions.Encryption(Constants.AUTH.REMEMBER_USER_KEY);
                string? json = encrypt.Decrypt(serialize);

                user = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.UserLoginDo>(json);
                if (user != null)
                    user.Password = "@@@Pwd123";
            }

            #endregion

            if (user == null)
                user = new Models.UserLoginDo();

            return Ok(new
            {
                User = user
            });
        }
        [HttpPost]
        [Route("Login")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> Login([FromBody] Models.UserLoginDo oUser)
        {
            string cookieName = string.Format("{0}:{1}", oUser.AppCode, Constants.AUTH.REMEMBER_USER_NAME);

            ApplicationUser? appUser = await this.userManager.FindByNameAsync(oUser.UserName);

            if (appUser == null)
                return NotFound("E0076");


            bool flagActive = appUser.ActiveFlag;
            if (flagActive == true)
            {
                var roles = await this.userManager.GetRolesAsync(appUser);
                if (roles.Count > 0)
                {
                    foreach (string role in roles)
                    {
                        ApplicationRole? appRole = await this.roleManager.FindByNameAsync(role);
                        if (appRole != null)
                        {
                            flagActive = appRole.ActiveFlag;
                            if (flagActive == false)
                                break;
                        }
                    }
                }
            }
            if (flagActive == false)
                return Unauthorized("E0004");

            if (appUser.FirstLoginFlag == true)
                return Unauthorized("E0073");

            if (oUser.Remember == true && oUser.Password == "@@@Pwd123")
            {
                string serialize = "";
                if (Request.Cookies.TryGetValue(cookieName, out serialize))
                {
                    Utils.Extensions.Encryption encrypt = new Utils.Extensions.Encryption(Constants.AUTH.REMEMBER_USER_KEY);
                    string? json = encrypt.Decrypt(serialize);

                    Models.UserLoginDo? rUser = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.UserLoginDo>(json);
                    if (rUser != null)
                        oUser.Password = rUser.Password;
                }
            }

            int remindPasswordExpired = 0;
            bool isExpired = false;

            if (appUser.TwoFactorEnabled == false)
            {
                Microsoft.AspNetCore.Identity.SignInResult res
                    = await this.signInManager.PasswordSignInAsync(oUser.UserName, oUser.Password, false, true);
                if (res.Succeeded)
                {
                    if (appUser.PasswordAge != null)
                    {
                        int diff = 0;
                        if (appUser.LastUpdatePasswordDate == null)
                            isExpired = true;
                        else
                        {
                            diff = appUser.LastUpdatePasswordDate.Value
                                        .AddDays(appUser.PasswordAge.Value)
                                        .CompareTo(Utils.Extensions.IOUtil.GetCurrentDateTime);
                            if (diff < 0)
                                isExpired = true;
                        }

                        if (isExpired == false
                            && appUser.LastLoginDate != null)
                        {
                            if (Utils.Constants.COMMON.REMIND_PASSWORD_DATE != null)
                            {
                                int remind = 0;
                                if (int.TryParse(Utils.Constants.COMMON.REMIND_PASSWORD_DATE, out remind))
                                {
                                    if (diff <= remind)
                                    {
                                        remindPasswordExpired = diff;
                                    }
                                }
                            }
                        }
                    }

                    if (isExpired)
                    {
                        await this.signInManager.SignOutAsync();
                        return Unauthorized("E0008");
                    }
                }
                else if (res.IsLockedOut)
                    return Unauthorized($"E0006:{AUTH.LOGIN_WAITING_TIME:N0}");
                else
                {
                    int accessFailedCount = await this.userManager.GetAccessFailedCountAsync(appUser);
                    int attemptsLeft = AUTH.MAXIMUM_LOGIN_FAIL - accessFailedCount;

                    return Unauthorized($"E0005:{attemptsLeft:N0}");
                }

                appUser.LastLoginDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
                await this.userManager.UpdateAsync(appUser);

                string token = await this.signInManager.GenerateToken(oUser.AppCode, appUser);
                string rtoken = await this.userManager.GenerateRefreshTokenAsync(appUser);

                //HttpClient client = new HttpClient();
                //Uri uri_concurrent = new Uri(Utils.Constants.COMMON.CONCURRENT_URL + "/api/concurrent/create");
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //HttpResponseMessage concurrentRes = await client.PostAsync(uri_concurrent, null);

                var userInfo = this.userManager.GetUserInfo(appUser);

                #region Set cookie

                //if (oUser.Remember == true)
                //{
                //    string json = Newtonsoft.Json.JsonConvert.SerializeObject(new Models.UserLoginDo()
                //    {
                //        UserName = appUser.UserName,
                //        Password = oUser.Password,
                //        Remember = true
                //    });

                //    Utils.Extensions.Encryption encrypt = new Utils.Extensions.Encryption(Constants.AUTH.REMEMBER_USER_KEY);
                //    string rememberCookie = encrypt.Encrypt(json);

                //    HttpContext.Response.Cookies.Append(
                //        cookieName,
                //        rememberCookie,
                //        new Microsoft.AspNetCore.Http.CookieOptions
                //        {
                //            Expires = DateTimeOffset.UtcNow.AddDays(7),
                //            HttpOnly = true
                //        }
                //    );
                //}
                //else
                //{
                //    if (HttpContext.Request.Cookies.ContainsKey(cookieName))
                //        HttpContext.Response.Cookies.Delete(cookieName);
                //}
                if (HttpContext.Request.Cookies.ContainsKey(cookieName))
                    HttpContext.Response.Cookies.Delete(cookieName);
                #endregion

                //if (concurrentRes.StatusCode == System.Net.HttpStatusCode.OK)
                if (userInfo != null)
                {
                    this.WriteRefreshToken(rtoken, appUser.Id);

                    return Ok(new
                    {
                        TwoFactorEnabled = appUser.TwoFactorEnabled,
                        Id = appUser.Id,
                        UserNumber = userInfo.UserNumber,
                        UserName = appUser.UserName,
                        DisplayName = userInfo.Name.ToUpper(),
                        Token = token,
                        RefreshToken = rtoken,
                        Timeout = Convert.ToDouble(this.configuration["JwtExpireMinutes"]),
                        RemindPasswordExpired = remindPasswordExpired
                    });
                }
                else
                {
                    //var _res = concurrentRes.Content.ReadAsStringAsync().Result;
                    //ErrorConCurrentResponse jsonRes = JsonSerializer.Deserialize<ErrorConCurrentResponse>(_res);
                    return Unauthorized("Unauthorized");
                }
            }

            if (await this.userManager.IsLockedOutAsync(appUser))
                return Unauthorized($"E0006:{AUTH.LOGIN_WAITING_TIME:N0}");

            if (await this.userManager.CheckPasswordAsync(appUser, oUser.Password) == false)
            {
                await this.userManager.AccessFailedAsync(appUser);

                int accessFailedCount = await this.userManager.GetAccessFailedCountAsync(appUser);
                int attemptsLeft = AUTH.MAXIMUM_LOGIN_FAIL - accessFailedCount;

                return Unauthorized($"E0005:{attemptsLeft:N0}");
            }

            if (appUser.PasswordAge != null)
            {
                if (appUser.LastUpdatePasswordDate == null)
                    isExpired = true;
                else
                {
                    int diff = appUser.LastUpdatePasswordDate.Value
                                .AddDays(appUser.PasswordAge.Value)
                                .CompareTo(Utils.Extensions.IOUtil.GetCurrentDateTime);
                    if (diff < 0)
                        isExpired = true;
                }
            }

            if (isExpired)
                return Unauthorized("E0008");

            string authenticatorUrl = "";
            List<string> sharedKeys = new List<string>();

            if (remindPasswordExpired == 0)
            {
                string? unformattedKey = await this.userManager.GetAuthenticatorKeyAsync(appUser);
                if (string.IsNullOrEmpty(unformattedKey))
                {
                    await this.userManager.ResetAuthenticatorKeyAsync(appUser);
                    unformattedKey = await this.userManager.GetAuthenticatorKeyAsync(appUser);
                }

                ApplicationDo? app = this.service.GetApplication(new ApplicationDo()
                {
                    AppCode = oUser.AppCode
                });
                authenticatorUrl = string.Format(
                                        CultureInfo.InvariantCulture,
                                        "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6",
                                        this.urlEncoder.Encode(app?.AppName),
                                        this.urlEncoder.Encode(appUser.Email),
                                        unformattedKey);

                int currentPosition = 0;
                while (currentPosition + 4 < unformattedKey.Length)
                {
                    sharedKeys.Add(unformattedKey.Substring(currentPosition, 4).ToLower());
                    currentPosition += 4;
                }
                if (currentPosition < unformattedKey.Length)
                {
                    sharedKeys.Add(unformattedKey.Substring(currentPosition).ToLower());
                }
            }

            return Ok(new
            {
                TwoFactorEnabled = appUser.TwoFactorEnabled,
                AuthenticatorUrl = authenticatorUrl,
                SharedKeys = sharedKeys
            });
        }
        [HttpPost]
        [Route("VerifyLogin")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> VerifyLogin([FromBody] Models.UserLoginDo oUser)
        {
            string cookieName = string.Format("{0}:{1}", oUser.AppCode, Constants.AUTH.REMEMBER_USER_NAME);
            ApplicationUser? appUser = await this.userManager.FindByNameAsync(oUser.UserName);
            if (appUser == null)
                return NotFound("E0003");

            bool flagActive = appUser.ActiveFlag;
            if (flagActive == true)
            {
                var roles = await this.userManager.GetRolesAsync(appUser);
                if (roles.Count > 0)
                {
                    foreach (string role in roles)
                    {
                        ApplicationRole? appRole = await this.roleManager.FindByNameAsync(role);
                        if (appRole != null)
                        {
                            flagActive = appRole.ActiveFlag;
                            if (flagActive == false)
                                break;
                        }
                    }
                }
            }
            if (flagActive == false)
                return Unauthorized("E0004");

            if (appUser.FirstLoginFlag == true)
                return Unauthorized("E0073");

            if (oUser.Remember == true && oUser.Password == "@@@Pwd123")
            {
                string serialize = "";
                if (Request.Cookies.TryGetValue(cookieName, out serialize))
                {
                    Utils.Extensions.Encryption encrypt = new Utils.Extensions.Encryption(Constants.AUTH.REMEMBER_USER_KEY);
                    string? json = encrypt.Decrypt(serialize);

                    Models.UserLoginDo? rUser = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.UserLoginDo>(json);
                    if (rUser != null)
                        oUser.Password = rUser.Password;
                }
            }

            //if (await this.userManager.VerifyTwoFactorTokenAsync(appUser, this.userManager.Options.Tokens.AuthenticatorTokenProvider, oUser.VerifyCode) == false)
            //    return Unauthorized("E0071");

            await this.signInManager.SignInAsync(appUser, false);
            await this.userManager.ResetAccessFailedCountAsync(appUser);

            int remindPasswordExpired = 0;
            bool isExpired = false;
            if (appUser.PasswordAge != null)
            {
                int diff = 0;
                if (appUser.LastUpdatePasswordDate == null)
                    isExpired = true;
                else
                {
                    diff = appUser.LastUpdatePasswordDate.Value
                                .AddDays(appUser.PasswordAge.Value)
                                .CompareTo(Utils.Extensions.IOUtil.GetCurrentDateTime);
                    if (diff < 0)
                        isExpired = true;
                }

                if (isExpired == false
                    && appUser.LastLoginDate != null)
                {
                    if (Utils.Constants.COMMON.REMIND_PASSWORD_DATE != null)
                    {
                        int remind = 0;
                        if (int.TryParse(Utils.Constants.COMMON.REMIND_PASSWORD_DATE, out remind))
                        {
                            if (diff <= remind)
                            {
                                remindPasswordExpired = diff;
                            }
                        }
                    }
                }
            }

            if (isExpired)
                return Unauthorized("E0008");

            appUser.LastLoginDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            await this.userManager.UpdateAsync(appUser);

            string token = await this.signInManager.GenerateToken(oUser.AppCode, appUser);
            string rtoken = await this.userManager.GenerateRefreshTokenAsync(appUser);

            var userInfo = this.userManager.GetUserInfo(appUser);

            #region Set cookie

            if (oUser.Remember == true)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new Models.UserLoginDo()
                {
                    UserName = appUser.UserName,
                    Password = oUser.Password,
                    Remember = true
                });

                Utils.Extensions.Encryption encrypt = new Utils.Extensions.Encryption(Constants.AUTH.REMEMBER_USER_KEY);
                string rememberCookie = encrypt.Encrypt(json);

                HttpContext.Response.Cookies.Append(
                    cookieName,
                    rememberCookie,
                    new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true
                    }
                );
            }
            else
            {
                if (HttpContext.Request.Cookies.ContainsKey(cookieName))
                    HttpContext.Response.Cookies.Delete(cookieName);
            }

            #endregion

            return Ok(new
            {
                TwoFactorEnabled = appUser.TwoFactorEnabled,
                Id = appUser.Id,
                UserNumber = userInfo.UserNumber,
                UserName = appUser.UserName,
                DisplayName = userInfo.Name.ToUpper(),
                Token = token,
                RefreshToken = rtoken,
                Timeout = Convert.ToDouble(this.configuration["JwtExpireMinutes"]),
                RemindPasswordExpired = remindPasswordExpired
            });
        }

        [HttpPost]
        [Route("RefreshToken")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> RefreshToken([FromBody] Models.RefreshTokenDo oToken)
        {
            if (oToken.UserName != null
                    && oToken.RefreshToken != null)
            {
                ApplicationUser? appUser = await this.userManager.FindByNameAsync(oToken.UserName);

                if (appUser != null)
                {
                    if (await this.userManager.VerifyRefreshTokenAsync(appUser, oToken.RefreshToken) == true)
                    {
                        string token = await this.signInManager.GenerateToken(oToken.AppCode, appUser);
                        string rtoken = await this.userManager.GenerateRefreshTokenAsync(appUser);

                        return Ok(new
                        {
                            Token = token,
                            RefreshToken = rtoken,
                            Timeout = Convert.ToDouble(this.configuration["JwtExpireMinutes"])
                        });
                    }
                }

            }

            return Unauthorized();
        }

        [HttpGet]
        [Route("InitialUser")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> InitialUser()
        {
            DateTime createDate = Utils.Extensions.IOUtil.GetCurrentDateTime;
            int createBy = 1;


            await this.roleManager.CreateAsync(new RoleDo
            {
                AppCode = "MES",
                RoleName = "Administrator",
                Description = null,
                ActiveFlag = true,
                Permissions = new List<RolePermissionDo>()
                {
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS020",
                        PermissionCode = "OPN"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS030",
                        PermissionCode = "OPN"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS031",
                        PermissionCode = "OPN"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS031",
                        PermissionCode = "ADD"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS031",
                        PermissionCode = "EDT"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS031",
                        PermissionCode = "DEL"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS040",
                        PermissionCode = "OPN"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS041",
                        PermissionCode = "OPN"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS041",
                        PermissionCode = "ADD"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS041",
                        PermissionCode = "EDT"
                    },
                    new RolePermissionDo()
                    {
                        ScreenId = "SSS041",
                        PermissionCode = "DEL"
                    }
                },

                CreateDate = createDate,
                CreateBy = createBy
            });

            await this.userManager.CreateAsync(new UpdateUserDo()
            {
                AppCode = "superadmin",
                UserName = "admin",
                Password = "P@ssw0rd",
                FirstName = "superadmin",
                LastName = "Administrator",
                Email = "superadmin@csithai.com",
                Remark = null,
                ActiveFlag = true,
                Roles = new List<UpdateUserRole>()
                {
                    new UpdateUserRole()
                    {
                        RoleName = "Administrator"
                    }
                },

                CreateDate = createDate,
                CreateBy = createBy
            });
            return Ok();
        }

        #region Forget password

        [HttpPost]
        [Route("ResetPassword")]
        [TypeFilter(typeof(ActionExceptionFilter))]
        public async Task<IActionResult> ResetPassword([FromBody] UpdateUserDo oUser)
        {
            ApplicationUser? appUser = null;
            if (oUser.Id != null)
                appUser = await this.userManager.FindByIdAsync(oUser.Id);
            else if (oUser.Email != null)
                appUser = await this.userManager.FindByEmailAsync(oUser.Email);

            if (appUser == null)
                return BadRequest("E0070");

            string token = await this.userManager.GeneratePasswordResetTokenAsync(appUser);

            string templatePath = System.IO.Path.Combine("Templates", "Template.ResetPassword.html");
            if (System.IO.File.Exists(templatePath))
            {
                string url = string.Format("{0}?i={1}&t={2}", AUTH.RESET_PASSWORD_URL, appUser.Id, System.Web.HttpUtility.UrlEncode(token));

                string contentHTML = System.IO.File.ReadAllText(templatePath);
                contentHTML = contentHTML.Replace("{Username}", appUser.UserName);
                contentHTML = contentHTML.Replace("{Url}", url);

                await this.emailService.SendAsync(new Utils.Models.EmailMessageDo()
                {
                    Subject = "Reset Password",
                    To = appUser.Email,
                    Body = contentHTML
                });
            }

            return Ok(true);
        }
        [HttpPost]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById([FromBody] Models.ConfirmResetPasswordDo oUser)
        {
            ApplicationUser? appUser = await this.userManager.FindByIdAsync(oUser.Id);
            if (appUser == null)
                return Unauthorized("E0074");

            return Ok(new
            {
                FirstLoginFlag = appUser.FirstLoginFlag,
                Email = appUser.Email
            });
        }
        [HttpPost]
        [Route("ConfirmResetPassword")]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] Models.ConfirmResetPasswordDo oUser)
        {
            ApplicationUser? appUser = await this.userManager.FindByEmailAsync(oUser.Email);
            if (appUser != null)
            {
                var resetRes = await this.userManager.ResetPasswordAsync(appUser, oUser.Token, oUser.Password);
                if (resetRes.Succeeded == true)
                {
                    if (appUser.FirstLoginFlag == true)
                    {
                        appUser.FirstLoginFlag = false;
                        await this.userManager.UpdateAsync(appUser);
                    }
                    return Ok(true);
                }
            }

            return BadRequest("E0069");
        }

        #endregion

        #region WriteRefreshToken
        private void WriteRefreshToken(string refresh_token, string id)
        {
            try
            {
                string path = Utils.Constants.COMMON.TEMP_PATH;
                path = System.IO.Path.Combine(path, "token_storage");
                if (System.IO.Directory.Exists(path) == false)
                    System.IO.Directory.CreateDirectory(path);
                path = System.IO.Path.Combine(path, refresh_token);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                using (System.IO.StreamWriter wr = new System.IO.StreamWriter(path, true))
                {
                    wr.WriteLine(id);
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}