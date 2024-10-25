using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Products;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // services.AddAutoMapper(Mapper => Mapper.AddProfile(new MappingProfile()));
            // services.AddAutoMapper(Mapper => Mapper.AddProfile<MappingProfile>());
            services.AddAutoMapper(typeof(MappingProfile));
            // services.AddAutoMapper(typeof(MappingProfile).Assembly);
            // services.AddAutoMapper(typeof(AssemblyInformation).Assembly);
            services.AddScoped(typeof(IProductService), typeof(ProductServices));
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            //services.AddScoped(typeof(IBasketService), (typeof(BasketService));
            //services.AddScoped(typeof(Func<IBasketService>), typeof(Func<BasketService>));
            services.AddScoped(typeof(IBasketService), typeof(BasketService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
            {
                //var BasketRepository = serviceProvider.GetRequiredService<IBasketRepository>();
                //var mapper = serviceProvider.GetRequiredService<IMapper>();
                //var configurations = serviceProvider.GetRequiredService<IConfiguration>();

                //return () => new BasketService(BasketRepository, mapper, configurations);
                return () => serviceProvider.GetService<IBasketService>();
            });
            return services;
        }
    }
}
