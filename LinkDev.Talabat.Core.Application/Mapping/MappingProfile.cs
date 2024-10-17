using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using static System.Net.WebRequestMethods;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(PDto => PDto.Brand, config => config.MapFrom(P => P.Brand!.Name))
                .ForMember(PDto => PDto.Category, config => config.MapFrom(P => P.Category!.Name))
                //.ForMember(PDto => PDto.PictureUrl, config => config.MapFrom(S => $"{"https://localhost:7018"}{S.PictureUrl}"))
                .ForMember(PDto => PDto.PictureUrl, Config => Config.MapFrom<ProductPictreUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();


            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
