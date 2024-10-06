using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(this StoreContext storecontext)
        {
            if (!storecontext.Brands.Any())
            {
                var brandsFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsFile);
                if (Brands?.Count() > 0)
                {
                    await storecontext.Brands.AddRangeAsync(Brands);
                    await storecontext.SaveChangesAsync();
                }
            }

            if (!storecontext.Categories.Any())
            {
                var categoriesFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesFile);
                if (Categories?.Count() > 0)
                {
                    await storecontext.Categories.AddRangeAsync(Categories);
                    await storecontext.SaveChangesAsync();
                }
            }
            if (!storecontext.Products.Any())
            {
                var ProductsFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsFile);
                if (Products?.Count() > 0)
                {
                    await storecontext.Products.AddRangeAsync(Products);
                    await storecontext.SaveChangesAsync();
                }
            }
        }
    }
}
