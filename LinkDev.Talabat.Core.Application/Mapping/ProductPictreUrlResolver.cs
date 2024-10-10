using AutoMapper;
using AutoMapper.Execution;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    internal class ProductPictreUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDto, string?>
    {

        public string? Resolve(Product source, ProductToReturnDto destination, string? destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["Urls:ApiBaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty ;   
        }
    }
}
