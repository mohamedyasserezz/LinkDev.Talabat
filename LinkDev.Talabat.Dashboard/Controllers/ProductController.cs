using AutoMapper;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Dashboard.Common;
using LinkDev.Talabat.Dashboard.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class ProductController(IUnitOfWork _unitOfWork, IMapper _mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var mappedProduct = _mapper.Map<IReadOnlyList<ProductViewModel>>(product);
            return View(mappedProduct);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.Image != null)
                {
                    productViewModel.PictureUrl = PictureSettings.UploadFile(productViewModel.Image, "products");
                }
                else
                    productViewModel.PictureUrl = "images/products/iced-matcha.png";

                var product = _mapper.Map<Product>(productViewModel);

                await _unitOfWork.GetRepository<Product, int>().AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }
            return View(productViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var productVM = _mapper.Map<ProductViewModel>(product);
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                throw new NotFoundException(nameof(Product), id);
            if (ModelState.IsValid)
            {
                if (productViewModel.Image is not null && productViewModel.PictureUrl is not null)
                {
                    PictureSettings.DeleteFile(productViewModel.PictureUrl, "products");

                    productViewModel.PictureUrl = PictureSettings.UploadFile(productViewModel.Image, "products");
                }
                else
                    productViewModel.PictureUrl = PictureSettings.UploadFile(productViewModel.Image, "products");
                var product = _mapper.Map<Product>(productViewModel);

                 _unitOfWork.GetRepository<Product, int>().Update(product);    
                
                var result = await _unitOfWork.CompleteAsync();

                if(result > 0)
                    return RedirectToAction("Index");
            }
            return View(productViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var productVM = _mapper.Map<ProductViewModel>(product);
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                throw new NotFoundException(nameof(Product), id);

            try
            {
                var product = _mapper.Map<Product>(productViewModel);
                if(product.PictureUrl is not null)
                {
                    PictureSettings.DeleteFile(product.PictureUrl, "products");
                }
                _unitOfWork.GetRepository<Product, int>().Delete(product);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(productViewModel);
            }
        }
    }
}
