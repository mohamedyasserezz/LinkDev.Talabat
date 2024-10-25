using LinkDev.Talabat.Core.Domain.Contract.Persistance.DbInitializer;
namespace LinkDev.Talabat.Infrastructure.Persistence.Common
{
    public abstract class DbInitializer(DbContext dbContext) : IDbInitializer
    {
        public virtual async Task InitializeAsync()
        {
            var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (PendingMigrations.Any())
                await dbContext.Database.MigrateAsync();
        }

        public abstract Task SeedAsync();
    }
}
