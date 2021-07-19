using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationHandlers
{

    public class BCryptPasswordHasherOptions
    {
        public int WorkFactor { get; set; }
        public bool EnhancedEntropy { get; set; }

    }

    // dotnet add package BCrypt.Net-Next
    public class BCryptPasswordHasher<TUser> : IPasswordHasher<TUser>
        where TUser : class
    {
        private readonly BCryptPasswordHasherOptions options;

        public BCryptPasswordHasher(IOptions<BCryptPasswordHasherOptions> options)
        {
            this.options = options.Value;
        }

        public string HashPassword(TUser user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, options.WorkFactor, options.EnhancedEntropy);
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            bool isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

            return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
