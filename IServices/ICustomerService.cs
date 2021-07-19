using Models;
using System;
using System.Collections.Generic;

namespace IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();

        Customer Get(string username);
    }
}
