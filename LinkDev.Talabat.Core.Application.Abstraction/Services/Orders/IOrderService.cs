using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);


        Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);
        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId);

        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    }
}
