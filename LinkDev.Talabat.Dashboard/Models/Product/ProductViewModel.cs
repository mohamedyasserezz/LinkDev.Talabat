using LinkDev.Talabat.Core.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.Models.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(1,int.MaxValue)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Product Category id is required")]
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        [Required(ErrorMessage = "Product Brand id is required")]
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
    }
}
