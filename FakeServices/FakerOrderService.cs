using Bogus;
using IServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeServices
{
    public class FakerOrderService : IOrderService
    {
        private readonly ICollection<Order> orders;

        public FakerOrderService(Faker<Order> faker)
        {
            orders = faker.Generate(100);
        }

        public IEnumerable<Order> Get()
        {
            return orders;
        }

        public IEnumerable<Order> Get(string username)
        {
            return orders.Where(p => p.Customer.Username == username);
        }
    }
}
