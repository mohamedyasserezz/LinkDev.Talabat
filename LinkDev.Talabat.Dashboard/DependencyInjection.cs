using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Dashboard.Mapping;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDashboardServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(optionsBuilder =>
            {
                optionsBuilder
                .UseSqlServer(configuration.GetConnectionString("StoreContext"))
                .UseLazyLoadingProxies();
            });

          

            services.AddDbContext<StoreIdentityDbContext>(optionsBuilder =>
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;

                identityOptions.SignIn.RequireConfirmedAccount = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;


                /// identityOptions.Password.RequireNonAlphanumeric = true;
                /// identityOptions.Password.RequiredUniqueChars = 2;
                /// identityOptions.Password.RequiredLength = 6;
                /// identityOptions.Password.RequireDigit = true;
                /// identityOptions.Password.RequireLowercase = true;
                /// identityOptions.Password.RequireUppercase = true;


                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);


                //identityOptions.Stores
                //identityOptions.ClaimsIdentity
                //identityOptions.Tokens
            })
               .AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddAutoMapper(typeof(MapsProfile));
            return services;
        }
    }
}
