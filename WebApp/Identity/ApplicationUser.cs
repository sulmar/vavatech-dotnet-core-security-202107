using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName, string firstName, string lastName, string department)
            : base(userName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Department = department;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }
}
