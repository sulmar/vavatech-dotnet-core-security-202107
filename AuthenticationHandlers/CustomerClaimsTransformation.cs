using IServices;
using Microsoft.AspNetCore.Authentication;
using Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationHandlers
{
    public class CustomerClaimsTransformation : IClaimsTransformation
    {
        private readonly ICustomerService customerService;

        public CustomerClaimsTransformation(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Kopia bieżącej tożsamości
            ClaimsPrincipal clonePrincipal = principal.Clone();

            ClaimsIdentity identity = (ClaimsIdentity)clonePrincipal.Identity;

            string username = identity.FindFirst(ClaimTypes.Name).Value;

            Customer customer = customerService.Get(username);

            identity.AddClaim(new Claim("Kategoria", "B"));
            identity.AddClaim(new Claim("Kategoria", "C"));
            identity.AddClaim(new Claim("Kategoria", "T"));

            identity.AddClaim(new Claim(ClaimTypes.Email, customer.Email));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, customer.PhoneNumber));
            identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, customer.DateOfBirth.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Gender, customer.Gender.ToString()));

            identity.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Developer"));


            return Task.FromResult(clonePrincipal);


        }
    }
}
