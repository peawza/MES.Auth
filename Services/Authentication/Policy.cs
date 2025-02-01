using Microsoft.AspNetCore.Authorization;

namespace Authentication
{
    public class AuthorizePolicy
    {
        public static void SetPolicy(AuthorizationOptions options)
        {
            options.AddPolicy("SSS031-OpenUser", policy => {
                policy.RequireAssertion(context => context.User.HasClaim(c =>
                    (c.Type == "Permission" && c.Value.IndexOf(".SSS031.OPN") >= 0)));
            });
            options.AddPolicy("SSS031-AddUser", policy => {
                policy.RequireAssertion(context => context.User.HasClaim(c =>
                    (c.Type == "Permission" && c.Value.IndexOf(".SSS031.ADD") >= 0)));
            });
            options.AddPolicy("SSS031-UpdateUser", policy => {
                policy.RequireAssertion(context => context.User.HasClaim(c =>
                    (c.Type == "Permission" && c.Value.IndexOf(".SSS031.EDT") >= 0)));
            });
            options.AddPolicy("SSS031-DeleteUser", policy => {
                policy.RequireAssertion(context => context.User.HasClaim(c =>
                    (c.Type == "Permission" && c.Value.IndexOf(".SSS031.DEL") >= 0)));
            });
        }
    }
}
