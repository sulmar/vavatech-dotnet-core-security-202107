using Bogus;
using Models;
using System;
using System.Collections.Generic;

namespace Fakers
{
    public class OrderFaker : Faker<Order>
    {
        public OrderFaker(Faker<Customer> customerFaker)
        {
            var customers = customerFaker.Generate(5);

            UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Customer, f => f.PickRandom(customers));
            RuleFor(p => p.TotalAmount, f => Math.Round(f.Random.Decimal(1, 1000), 0));
        }
    }
}
