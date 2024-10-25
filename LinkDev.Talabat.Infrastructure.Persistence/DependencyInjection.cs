using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using LinkDev.Talabat.Core.Domain.Contract.Persistance.DbInitializer;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region StoreContext
        services.AddDbContext<StoreDbContext>(optionsBuilder =>
        {
            optionsBuilder
            .UseSqlServer(configuration.GetConnectionString("StoreContext"))
            .UseLazyLoadingProxies();
        });
        services.AddScoped<IStoreDbInitializer, StoreDbInitializer>();
        services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSavaChangesInterceptor)); 
        #endregion

        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

        #region IdentityDbContext
        services.AddDbContext<StoreIdentityDbContext>(optionsBuilder =>
        {
            optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
        });

        services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));
        #endregion

        return services;
    }
}

