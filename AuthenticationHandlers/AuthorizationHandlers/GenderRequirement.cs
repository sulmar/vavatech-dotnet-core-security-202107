using Microsoft.AspNetCore.Authorization;
using Models;

namespace AuthenticationHandlers
{
    public class GenderRequirement : IAuthorizationRequirement
    {
        public Gender Gender { get; }

        public GenderRequirement(Gender gender)
        {
            Gender = gender;
        }
    }

    public static class GenderRequirementExtensions
    {
        public static AuthorizationPolicyBuilder RequireGender(this AuthorizationPolicyBuilder policy, Gender gender)
        {
            policy.Requirements.Add(new GenderRequirement(gender));

            return policy;
        }
    }




}
