using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Identity;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(p =>
            {
                p.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
                p.Property(p => p.LastName).HasMaxLength(50).IsRequired();
                p.Property(p => p.Department).HasMaxLength(50).IsRequired();
            }); 
        }
    }
}
