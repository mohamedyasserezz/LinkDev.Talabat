using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetCustomerBasketAsync(string id);
        Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasketDto);
        Task DeleteCustomerBasketAsync(string id);

    }
}
