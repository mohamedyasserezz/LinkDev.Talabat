using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Produts
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecification<Product, int>

    {
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categorId, int pageSize, int PageIndex) :
            base(
                P =>
                (!brandId.HasValue || brandId == P.BrandId)
                &&
                (!categorId.HasValue || categorId == P.CategoryId)
                )
        {
            AddIncludes();
            
                switch (sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(P => P.Name);
                        break;
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            
            ApplyPagination(pageSize * (PageIndex - 1), pageSize);
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            AddIncludes();
        }
        private protected override void AddIncludes()
        {
            Includes.Add(P => P.Category!);
            Includes.Add(P => P.Brand!);
        }

    }
}

