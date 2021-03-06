using AuthenticationHandlers;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    [Authorize]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        { 
            if (User.IsInRole("Administrator"))
            {
                var orders = orderService.Get();
                return Ok(orders);
            }
            else
            {
                var orders = orderService.Get(User.Identity.Name);
                return Ok(orders);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id, 
            [FromServices] Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService)
        {
            Order order = orderService.Get(id);

            var result  = await authorizationService.AuthorizeAsync(User, order, new TheSameAuthorRequirment());

            if (result.Succeeded)
            {
                return Ok(order);
            }
            else if (User.Identity.IsAuthenticated)
            {
                return Forbid();
            }
            else
            {
                return Challenge();
            }

            
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            string email = User.FindFirst(ClaimTypes.Email)?.Value;
            string phoneNumber = User.FindFirst(ClaimTypes.MobilePhone)?.Value;

            return Ok(order);

        }
    }
}
