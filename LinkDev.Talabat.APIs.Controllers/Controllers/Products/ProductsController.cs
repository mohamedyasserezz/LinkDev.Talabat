using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProductAsync([FromQuery] ProductSpecParams specParams)
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(specParams);
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);
            //if (product is null)
            //    return NotFound(new { statusCode = 404, message = "NotFound." });

            return Ok(product);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandAsync()
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();
            if (brands is null)
                return NotFound();
            return Ok(brands);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesAsync()
        {
            var categories = await serviceManager.ProductService.GetCategorysAsync();
            if (categories is null)
                return NotFound();
            return Ok(categories);
        }
    }
}
