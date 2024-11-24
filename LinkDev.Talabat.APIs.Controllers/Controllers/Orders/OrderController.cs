using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Orders
{
    [Authorize]
    public class OrderController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpPost]

        public async Task<ActionResult<OrderToCreateDto>> CreateOrder(OrderToCreateDto orderToCreateDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = serviceManager.OrderService.CreateOrderAsync(buyerEmail!, orderToCreateDto);

            return Ok(result);
        }
    }
}
