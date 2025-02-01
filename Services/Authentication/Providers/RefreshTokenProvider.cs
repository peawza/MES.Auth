using Authentication.Constants;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Authentication.Providers
{
    public class RefreshTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public RefreshTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<RefreshTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider, options, logger)
        {
        }
    }
    public class RefreshTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public RefreshTokenProviderOptions()
        {
            Name = "RefreshToken";
            TokenLifespan = TimeSpan.FromMinutes(AUTH.REFRESH_TOKEN_EXPIRE_MINUTES);

        }
    }
}
