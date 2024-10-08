using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
        });
        services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
        services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSavaChangesInterceptor));
        return services;
    }
}

