using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationHandlers
{
    // dotnet add package BCrypt.Net-Next
    public class BCryptPasswordHasher<TUser> : IPasswordHasher<TUser>
        where TUser : class
    {
        public string HashPassword(TUser user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            bool isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

            return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
