using Bogus;
using Ganss.XSS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSSAttack.Fakers;
using XSSAttack.FakeServices;
using XSSAttack.IServices;
using XSSAttack.Middlewares;
using XSSAttack.Models;

namespace XSSAttack
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
            services.AddControllersWithViews();

            services.AddSingleton<Faker<Post>, PostFaker>();
            services.AddSingleton<IPostRepository, FakePostRepository>();

            services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();

            app.UseStaticFiles();

            // dotnet add package NWebsec.AspNetCore.Middleware

            // app.UseXfo(options => options.SameOrigin());

            // https://securityheaders.com/
            // https://www.zaproxy.org/

            app.UseMiddleware<AntiDOSMiddleware>(10, 5000);

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");

                await next();
            });

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "script-src 'self'");
                await next();
            });


            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                await next();
            });


            

            app.UseRouting();

            app.UseMiddleware<AntiXssMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
