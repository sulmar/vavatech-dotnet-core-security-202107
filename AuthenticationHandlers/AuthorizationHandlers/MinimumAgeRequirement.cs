using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationHandlers
{
    public class MinimumAgeRequirement : IAuthorizationRequirement // mark interface
    {
        public int MinimumAge { get; }

        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }

    public static class MinimumAgeRequirementExtensions
    {
        public static AuthorizationPolicyBuilder RequireAge(this AuthorizationPolicyBuilder policy, int age)
        {
            policy.Requirements.Add(new MinimumAgeRequirement(age));

            return policy;
        }
    }
        




}
