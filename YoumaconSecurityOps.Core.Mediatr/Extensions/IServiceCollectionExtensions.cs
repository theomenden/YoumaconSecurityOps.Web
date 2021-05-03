using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace YoumaconSecurityOps.Core.Mediatr.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatrServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(IServiceCollectionExtensions).Assembly);

            return services;
        }
    }
}
