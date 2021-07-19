using AuthenticationHandlers;
using Bogus;
using Fakers;
using FakeServices;
using IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.AddSingleton<IServices.IAuthorizationService, CustomerAuthorizationService>();

            services.AddSingleton<IOrderService, FakerOrderService>();
            services.AddSingleton<Faker<Order>, OrderFaker>();

            

            // services.AddTransient<IPasswordHasher<Customer>, PasswordHasher<Customer>>();

            services.AddTransient<IPasswordHasher<Customer>, BCryptPasswordHasher<Customer>>();

            services.Configure<BCryptPasswordHasherOptions>(c => c.WorkFactor = 11);

            services.AddAuthentication(defaultScheme: "Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("WomanAdult",
                    policy =>
                    {
                        // policy.Requirements.Add(new GenderRequirement(Gender.Female));
                        // policy.Requirements.Add(new MinimumAgeRequirement(18));

                        policy.RequireClaim(ClaimTypes.DateOfBirth);
                        policy.RequireClaim(ClaimTypes.Gender);

                        policy.RequireGender(Gender.Female);
                        policy.RequireAge(18);

                        
                    });

                options.AddPolicy("ManAdult",
                    policy =>
                    {
                        // policy.Requirements.Add(new GenderRequirement(Gender.Male));
                        // policy.Requirements.Add(new MinimumAgeRequirement(25));

                        policy.RequireClaim(ClaimTypes.DateOfBirth);
                        policy.RequireClaim(ClaimTypes.Gender);

                        policy.RequireRole("Administrator");

                        policy.RequireGender(Gender.Male);
                        policy.RequireAge(25);
                    });

                options.AddPolicy("TheSameAuthor", policy => policy.Requirements.Add(new TheSameAuthorRequirment()));
            });

            services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddScoped<IAuthorizationHandler, GenderHandler>();
            services.AddScoped<IAuthorizationHandler, OrderAuthorizationHandler>();

            services.AddScoped<IClaimsTransformation, CustomerClaimsTransformation>();

            

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
