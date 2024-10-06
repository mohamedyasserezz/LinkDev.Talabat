
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private readonly Lazy<IGenricRepository<Product, int>> _productRepository;
        private readonly Lazy<IGenricRepository<ProductBrand, int>> _brandRepository;
        private readonly Lazy<IGenricRepository<ProductCategory, int>> _categoryRepository;
        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _productRepository = new Lazy<IGenricRepository<Product, int>>(() => new GenericRepository<Product, int>(_storeContext));
            _categoryRepository = new Lazy<IGenricRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_storeContext));
            _brandRepository = new Lazy<IGenricRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_storeContext));
        }
        public IGenricRepository<Product, int> ProductRepository => _productRepository.Value;
        public IGenricRepository<ProductBrand, int> BrandRepository => _brandRepository.Value;
        public IGenricRepository<ProductCategory, int> CategoryRepository => _categoryRepository.Value;

        public async Task<int> CompleteAsync()
        {
            return await _storeContext.SaveChangesAsync();
        }

        public ValueTask DisposeAsync() => _storeContext.DisposeAsync();
    }
}
