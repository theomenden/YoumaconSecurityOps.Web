using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace YoumaconSecurityOps.Core.AutoMapper.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMappingServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
