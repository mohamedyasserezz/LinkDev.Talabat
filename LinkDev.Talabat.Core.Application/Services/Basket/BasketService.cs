using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contract.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services.Basket
{
    internal class BasketService(IBasketRepository basketRepository,
        IMapper mapper,
        IConfiguration configuration) : IBasketService
    {
        private readonly IBasketRepository _basketRepository = basketRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;

        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string id)
        {
            var basket = await _basketRepository.GetAsync(id);
            if (basket is null)
                throw new NotFoundException("Basket", id);
            var basketDto = _mapper.Map<CustomerBasketDto>(basket);
            return basketDto;
        }

        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(customerBasketDto);
            var timeToLive = TimeSpan.FromDays(double.Parse(_configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!));
            var updatedBasket = await _basketRepository.UpdateAsync(basket, timeToLive);

            if (updatedBasket is null)
                throw new BadRequestException("Can't Update, there is a problem with this basket");

            return customerBasketDto;

        }
        public async Task DeleteCustomerBasketAsync(string id)
        {
            var IsDeleted = await _basketRepository.DeleteAsync(id);
            if(!IsDeleted)
                throw new BadRequestException($"unable to delete this basket.");
        }

    }
}
