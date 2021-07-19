using System;
using System.Collections.Generic;

namespace Models
{
    public abstract class Base
    {

    }

    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Role : BaseEntity
    {
        public string Name { get; set; }
    }


    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Gender Gender { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public bool IsRemoved { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
