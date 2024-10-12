using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Domain.Contract;
using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications;
using LinkDev.Talabat.Core.Domain.Specifications.Produts;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
    public class ProductServices(IUnitOfWork _unitOfWork,
        IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetAllProductsAsync(ProductSpecParams specParams)
        {
            var specification = new ProductWithBrandAndCategorySpecifications(specParams.sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(specification);

            var productDto = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return productDto;

        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var specification = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(specification);
            var productDto = mapper.Map<ProductToReturnDto>(product);
            return productDto;
        }
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var specification = new BaseSpecification<ProductBrand, int>();

            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllWithSpecAsync(specification);

            var brandDto = mapper.Map<IEnumerable<BrandDto>>(brands);
            return brandDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategorysAsync()
        {
            var specification = new BaseSpecification<ProductCategory, int>();

            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllWithSpecAsync(specification);

            var categoryDto = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoryDto;
        }

    }
}
