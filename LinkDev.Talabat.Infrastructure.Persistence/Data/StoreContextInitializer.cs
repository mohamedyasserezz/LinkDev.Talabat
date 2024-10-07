﻿

using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public class StoreContextInitializer(StoreContext _storeContext) : IStoreContextInitializer
    {
        public async Task InitializeAsync()
        {
            var PendingMigrations = await _storeContext.Database.GetPendingMigrationsAsync();

            if (PendingMigrations.Any())
                await _storeContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            if (!_storeContext.Brands.Any())
            {
                var brandsFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsFile);
                if (Brands?.Count() > 0)
                {
                    await _storeContext.Brands.AddRangeAsync(Brands);
                    await _storeContext.SaveChangesAsync();
                }
            }
            if (!_storeContext.Categories.Any())
            {
                var categoriesFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesFile);
                if (Categories?.Count() > 0)
                {
                    await _storeContext.Categories.AddRangeAsync(Categories);
                    await _storeContext.SaveChangesAsync();
                }
            }
            if (!_storeContext.Products.Any())
            {
                var ProductsFile = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsFile);
                if (Products?.Count() > 0)
                {
                    await _storeContext.Products.AddRangeAsync(Products);
                    await _storeContext.SaveChangesAsync();
                }
            }
        }
    }
}
