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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.GetOrdersForUserAsync(buyerEmail);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = serviceManager.OrderService.GetOrderByIdAsync(buyerEmail!, id);
            return Ok(result);
        }

        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethod()
        {
            var result = await serviceManager.OrderService.GetDeliveryMethodsAsync();

            return Ok(result);
        }
    }
}
