using IServices;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationHandlers
{
    public class CustomerAuthorizationService : IAuthorizationService
    {
        private readonly ICustomerService customerService;

        public CustomerAuthorizationService(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public bool TryAuthenticate(string login, string password, out Customer customer)
        {
            customer = customerService.Get(login);

            if (customer != null)
            {
                return customer.HashedPassword == password;
            }
            else
                return false;
        }
    }
}
