using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenricRepository<Product, int> ProductRepository { get; set; }
        public IGenricRepository<ProductBrand, int> BrandRepository { get;set; }
        public IGenricRepository<ProductCategory, int> CategoryRepository { get; set; }
        public Task<int> CompleteAsync();
    }
}
