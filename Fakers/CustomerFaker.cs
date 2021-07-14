using Bogus;
using Models;
using System;

namespace Fakers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            UseSeed(1);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.Username, f => f.Person.UserName);
            RuleFor(p => p.HashedPassword, f => "12345");
            RuleFor(p => p.PhoneNumber, f => f.Person.Phone);
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
        }
    }

    public class OrderFaker : Faker<Order>
    {
        public OrderFaker(Faker<Customer> customerFaker)
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Customer, f => customerFaker.Generate());
        }
    }
}
