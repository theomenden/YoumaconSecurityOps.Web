using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using YoumaconSecurityOps.Core.AutoMapper.Profiles;

namespace YoumaconSecurityOps.Core.AutoMapper.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMappingServices(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new YoumaAutoMappingProfile());
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);


            return services;
        }
    }
}
