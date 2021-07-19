using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    // [Authorize(Roles = "Administrator,Trainer")]
    [AuthorizeRoles(AuthorizationLevel.Roles.Admin, AuthorizationLevel.Roles.Developer)]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
       
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var customers = customerService.Get();
                
                return Ok(customers);
            }
            else
            {
                return Unauthorized();
            }
        }


        [AllowAnonymous]
        [HttpGet("/about")]
        public IActionResult About()
        {
            return Ok("Hello World");
        }


        [Authorize(Policy = "ManAdult")]
        [HttpGet("/help")]
        public IActionResult Help()
        {
            return Ok("Help");
        }


    }
}
