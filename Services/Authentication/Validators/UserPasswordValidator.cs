using Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Validators
{
    public class UserPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager,
                                              TUser user,
                                              string password)
        {
            string userName = null;
            ApplicationUser auser = user as ApplicationUser;
            if (auser != null)
                userName = auser.UserName;

            return Task.Run(() =>
            {
                int passwordLength = 8;
                if (password.Length >= passwordLength)
                {
                    int foundCondition = 0;
                    if (password.Any(x => char.IsUpper(x))) //Uppercase
                        foundCondition++;
                    if (password.Any(x => char.IsLower(x))) //Lowercase
                        foundCondition++;
                    if (password.Any(x => char.IsDigit(x))) //Digit
                        foundCondition++;

                    string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
                    char[] specialCharactersArray = specialCharacters.ToCharArray();

                    int index = password.IndexOfAny(specialCharactersArray);
                    if (index >= 0)
                        foundCondition++;

                    if (userName != null)
                    {
                        string b1 = userName.ToUpper();
                        string b2 = password.ToUpper();

                        if (b2.IndexOf(b1) < 0)
                            foundCondition++;
                    }

                    if (foundCondition >= 5)
                        return IdentityResult.Success;
                }

                return IdentityResult.Failed(new IdentityError
                {
                    Code = "E0011"
                });
            });
        }
    }
}
