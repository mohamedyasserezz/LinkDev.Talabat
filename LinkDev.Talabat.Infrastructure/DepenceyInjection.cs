using LinkDev.Talabat.Core.Domain.Contract.Infrastructure;
using LinkDev.Talabat.Infrastructure.Basket_Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure
{
    public static class DepenceyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton(typeof(IConnectionMultiplexer) , servicesProvider =>
            {
                //var connectionString = configuration.GetSection("ConnectionStrings")["Redis"];
                var connectionString = configuration.GetConnectionString("Redis");
                var connectionMultiplexerObj =  ConnectionMultiplexer.Connect(connectionString!);
                return connectionMultiplexerObj;
            });

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            return services;
        }
    }
}
