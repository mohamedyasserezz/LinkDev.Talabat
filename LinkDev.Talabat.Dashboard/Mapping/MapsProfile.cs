using AutoMapper;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Dashboard.Models.Product;

namespace LinkDev.Talabat.Dashboard.Mapping
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
        }
    }
}
