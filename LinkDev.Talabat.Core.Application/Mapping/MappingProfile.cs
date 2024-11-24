using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
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

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictrureUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodDto>();


        }
    }
}
