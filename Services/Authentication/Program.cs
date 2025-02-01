using Application;
using Application.Models;
using Authentication;
using Authentication.Constants;
using Authentication.Providers;
using Authentication.Services;
using Authentication.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Utils.Startup.ConfigConstants(builder, typeof(Authentication.Constants.AUTH));

/* --- Set Database ---*/
var connectionString =
    builder.Configuration.GetConnectionString("DBConnection");
Application.ApplicationDbContext.InitialService(builder.Services, connectionString);
Application.SystemDbContext.InitialService(builder.Services, connectionString);

IdentityBuilder identity = builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Password settings
    options.Password.RequiredLength = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(AUTH.LOGIN_WAITING_TIME);
    options.Lockout.MaxFailedAccessAttempts = AUTH.MAXIMUM_LOGIN_FAIL;

    // User settings
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<ApplicationUserManager>()
    .AddRoleManager<ApplicationRoleManager>()
    .AddSignInManager<ApplicationSignInManager>()
    .AddPasswordValidator<UserPasswordValidator<ApplicationUser>>()
    .AddTokenProvider<RefreshTokenProvider<ApplicationUser>>("RefreshToken");

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
{
    o.TokenLifespan = TimeSpan.FromMinutes(AUTH.DATA_PROTECTION_TOKEN_TIMEOUT_MINUTES);
});

AuthenticationBuilder auth = Utils.Startup.ConfigAuthentication(builder);
Utils.Startup.ConfigRequestSize(builder);
Utils.Startup.ConfigCors(builder);
Utils.Startup.ConfigController(builder);
Utils.Startup.ConfigUtils(builder);
Utils.Startup.ConfigSwagger(builder);

/* --- Add Repository & Service ---*/
StartupService.InitialService(builder.Services);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
    });
#region Custom Identity

bool useMicrosoftAuth = false;
bool.TryParse(builder.Configuration["Authentication:Microsoft:IsEnabled"], out useMicrosoftAuth);
if (useMicrosoftAuth)
{
    auth.AddMicrosoftAccount(options =>
    {
        string tenantId = builder.Configuration["Authentication:Microsoft:TenantId"];
        if (string.IsNullOrEmpty(tenantId) == false)
        {
            options.AuthorizationEndpoint = options.AuthorizationEndpoint.Replace("common", tenantId);
            options.TokenEndpoint = options.TokenEndpoint.Replace("common", tenantId);
        }

        options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];

        options.SaveTokens = true;
    });
}

bool useFacebookAuth = false;
bool.TryParse(builder.Configuration["Authentication:Facebook:IsEnabled"], out useFacebookAuth);
if (useFacebookAuth)
{
    auth.AddFacebook(options =>
    {
        options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
        options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
    });
}

bool useGoogleAuth = false;
bool.TryParse(builder.Configuration["Authentication:Google:IsEnabled"], out useGoogleAuth);
if (useGoogleAuth)
{
    auth.AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });
}

#endregion

builder.Services.AddAuthorization(AuthorizePolicy.SetPolicy);

var app = builder.Build();

Utils.Startup.UseCors(app);
Utils.Startup.UseSwagger(builder, app);
Utils.Startup.UseForwardedHeaders(app);

// app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
