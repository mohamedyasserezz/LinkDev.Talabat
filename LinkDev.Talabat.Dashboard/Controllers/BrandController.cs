using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class BrandController(IUnitOfWork _unitOfWork) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return View(brands);
        }
        public async Task<IActionResult> Create(ProductBrand brand)
        {
            try
            {
                await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(brand);

                await _unitOfWork.CompleteAsync(); 

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw new NotFoundException("Name", nameof(ProductBrand));
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
