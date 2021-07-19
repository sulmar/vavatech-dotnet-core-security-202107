using Bogus;
using IServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerService(Faker<Customer> faker)
        {
            customers = faker.Generate(100);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(string username)
        {
            return customers.SingleOrDefault(c => c.Username == username);
        }
    }
}
