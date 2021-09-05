using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using YoumaconSecurityOps.Web.Client.Toast.Core;
using YoumaconSecurityOps.Web.Client.Toast.Services;

namespace YoumaconSecurityOps.Web.Client.Toast.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddToastServices(this IServiceCollection services)
        {
            services.AddScoped<IToastService, ToastService>();

            return services;
        }
    }
}
