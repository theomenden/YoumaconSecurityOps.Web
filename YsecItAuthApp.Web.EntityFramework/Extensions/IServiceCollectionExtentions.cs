using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YoumaconSecurityOps.Core.Shared.Configuration;
using YsecItAuthApp.Web.EntityFramework.Context;
using YsecItAuthApp.Web.EntityFramework.Models;

namespace YsecItAuthApp.Web.EntityFramework.Extensions
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddDbContext<YsecItSecurityContext>(options =>
                options.UseSqlServer(appSettings.YSecItAuthConnectionString)
                    .EnableDetailedErrors()
                    .EnableServiceProviderCaching()
                );
            
            return services;
        }
    }
}
