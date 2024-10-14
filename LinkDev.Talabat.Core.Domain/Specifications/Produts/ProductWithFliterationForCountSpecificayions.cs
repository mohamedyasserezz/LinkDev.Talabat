using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Produts
{
    public class ProductWithFliterationForCountSpecificayions : BaseSpecification<Product, int>
    {
        public ProductWithFliterationForCountSpecificayions(int? brandId, int? categoryId,string? search)
            : base(

                P =>
                 (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
                &&
                (!brandId.HasValue || brandId == P.BrandId)
                &&
                (!categoryId.HasValue || categoryId == P.CategoryId)

                  )
        {

        }
    }
}
