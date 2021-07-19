using Microsoft.AspNetCore.Authorization;
using Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationHandlers
{
    public class GenderHandler : AuthorizationHandler<GenderRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GenderRequirement requirement)
        {
            if (!context.User.HasClaim(p => p.Type == ClaimTypes.Gender))
            {
                context.Fail();

                return Task.CompletedTask;
            }

            Gender gender = Enum.Parse<Gender>(context.User.FindFirstValue(ClaimTypes.Gender));

            if (gender == requirement.Gender)
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
