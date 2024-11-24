using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using LinkDev.Talabat.Core.Domain.Contract.Persistance.DbInitializer;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region StoreContext

        services.AddScoped(typeof(AuditInterceptor));
        services.AddDbContext<StoreDbContext>((serviceProvider, optionsBuilder) =>
        {
            optionsBuilder
            .UseSqlServer(configuration.GetConnectionString("StoreContext"))
            .UseLazyLoadingProxies()
            .AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>());
        });
        services.AddScoped<IStoreDbInitializer, StoreDbInitializer>();

        //services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSavaChangesInterceptor)); 

        #endregion

        #region IdentityDbContext
        services.AddDbContext<StoreIdentityDbContext>(optionsBuilder =>
        {
            optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
        });

        services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));
        #endregion

        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

        services.AddIdentityCore<ApplicationUser>();

        return services;
    }
}

