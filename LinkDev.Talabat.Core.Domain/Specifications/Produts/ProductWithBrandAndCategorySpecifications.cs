using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Produts
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecification<Product, int>
     
    {
        public ProductWithBrandAndCategorySpecifications() : base() 
        {
            Includes.Add(P => P.Category!);
            Includes.Add(P => P.Brand!);
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            Includes.Add(P => P.Category!);
            Includes.Add(P => P.Brand!);
        }
    }
}
