using LinkDev.Talabat.Core.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            return services;
        }
    }
}
