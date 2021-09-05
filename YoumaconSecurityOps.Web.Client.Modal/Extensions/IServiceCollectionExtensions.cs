using Microsoft.Extensions.DependencyInjection;
using YoumaconSecurityOps.Web.Client.Modal.Core;

namespace YoumaconSecurityOps.Web.Client.Modal.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddModalServices(this IServiceCollection services)
        {
            services.AddScoped<IModalService, ModalService>();

            return services;
        }
    }
}
