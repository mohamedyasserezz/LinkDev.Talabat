using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToReturnDto orderToReturnDto);

        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, string orderId);

        Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);

        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    }
}
