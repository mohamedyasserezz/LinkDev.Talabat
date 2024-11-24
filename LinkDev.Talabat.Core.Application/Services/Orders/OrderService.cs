using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
    internal class OrderService(IUnitOfWork unitOfWork, IBasketService basketService, IMapper mapper) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            // Get Customer Basket repo
            var basket = await basketService.GetCustomerBasketAsync(order.BasketId);

            // Get Selected Items at basket from product repo
            var orderItems = new List<OrderItem>();
            if (basket.Items.Count() > 0)
            {
                var productRepo = unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);

                    if (product is not null)
                    {

                        var productItemOrder = new ProductItemOrderded()
                        {
                            ProductId = product.Id,
                            PictureUrl = product.PictureUrl,
                            ProductName = product.Name,
                        };

                        var orderItem = new OrderItem()
                        {
                            Product = productItemOrder,
                            Price = product.Price,
                            Quantity = item.Quantity,
                        }; 

                        orderItems.Add(orderItem);
                    }
                }
            }

            // Culculate SubTotla
            var SubTotla = orderItems.Sum(items => items.Quantity * items.Price);
            
            // map address
            var address = mapper.Map<Address>(order.ShippingAddress);

            // create order

            var OrderToCreate = new Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                Subtotal = SubTotla,
                DeliveryMethodId = order.DeliveryMethodId,
                Items = orderItems,
            };
            await unitOfWork.GetRepository<Order,int>().AddAsync(OrderToCreate);

            // save to database
            var created = await unitOfWork.CompleteAsync() > 0;

            if (!created) throw new BadRequestException("an error has occured during creating the order");
            
            return mapper.Map<OrderToReturnDto>(OrderToCreate);
        }

        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
