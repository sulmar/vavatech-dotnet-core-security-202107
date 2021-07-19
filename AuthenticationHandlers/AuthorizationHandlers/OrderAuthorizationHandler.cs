using Microsoft.AspNetCore.Authorization;
using Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationHandlers
{

    public class TheSameAuthorRequirment : IAuthorizationRequirement
    {

    }

    public class OrderAuthorizationHandler : AuthorizationHandler<TheSameAuthorRequirment, Order>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TheSameAuthorRequirment requirement, Order resource)
        {
            string username = context.User.FindFirstValue(ClaimTypes.Name);

            if (resource.Customer.Username == username)
            {
                context.Succeed(requirement);
            }
            else
                context.Fail();


            return Task.CompletedTask;
        }
    }
}
