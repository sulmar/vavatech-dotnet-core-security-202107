using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationHandlers
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(p => p.Type == ClaimTypes.DateOfBirth))
            {
                context.Fail();

                return Task.CompletedTask;
            }

           DateTime dateOfBirth = Convert.ToDateTime(context.User.FindFirstValue(ClaimTypes.DateOfBirth));

           int age = DateTime.Today.Year - dateOfBirth.Year;

            if (age >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
               
        }
    }




}
