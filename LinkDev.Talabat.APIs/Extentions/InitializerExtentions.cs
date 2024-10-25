using LinkDev.Talabat.Core.Domain.Contract.Persistance.DbInitializer;
using LinkDev.Talabat.Infrastructure.Persistence.Data;

namespace LinkDev.Talabat.APIs.Extentions
{
    public static class InitializerExtentions
    {
        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var Services = scope.ServiceProvider;
            var storeContextInitializer = Services.GetRequiredService<IStoreDbInitializer>();
            var identityContextInitializer = Services.GetRequiredService<IStoreIdentityDbInitializer>();

            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                await storeContextInitializer.InitializeAsync();
                await storeContextInitializer.SeedAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an erroe have been occured while applying migrations or data seeding");
            }
            finally
            {
                // scope.Dispose();
            }
            return app;
        }
    }
}
