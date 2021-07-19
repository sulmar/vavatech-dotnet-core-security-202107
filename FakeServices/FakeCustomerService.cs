using Bogus;
using IServices;
using Models;
using System;
using System.Collections.Generic;

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
    }
}
