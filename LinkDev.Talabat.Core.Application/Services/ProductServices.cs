using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Domain.Contract;
using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ProductServices(IUnitOfWork _unitOfWork,
        IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetAllProductsAsync()
        {
           var products = await  _unitOfWork.GetRepository<Product,int>().GetAllAsync();
            
            var productDto = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return productDto;
            
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product,int>().GetAsync(id);
            var productDto = mapper.Map<ProductToReturnDto>(product);
            return productDto;
        }
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var brandDto = mapper.Map<IEnumerable<BrandDto>>(brands);
            return brandDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategorysAsync()
        {
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

            var categoryDto = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoryDto;
        }

    }
}
