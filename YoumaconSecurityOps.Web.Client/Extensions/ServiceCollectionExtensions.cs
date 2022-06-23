using YsecOps.Core.Mediator.Extensions;
using YSecOps.Data.EfCore.Extensions;
using YSecOps.Events.EfCore.Extensions;

namespace YoumaconSecurityOps.Web.Client.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers application defined services in our <paramref name="services"/> using established <paramref name="appSettings"/>
    /// </summary>
    /// <param name="services">Provided service collection</param>
    /// <param name="appSettings">Provided configurations</param>
    /// <returns><see cref="IServiceCollection"/> for further registration chaining</returns>
    public static IServiceCollection AddApplicationRegistrations(this IServiceCollection services, AppSettings appSettings) =>
        services
            .AddYSecEventStoreServices(appSettings.EventStoreConnectionString)
            .AddYSecDataServices(appSettings.YoumaDbConnectionString)
            .AddYsecMediatorServices<Program>()
            .AddIndexedDbServices();

    /// <summary>
    /// Registers Azure dependent services in our <paramref name="services"/>
    /// </summary>
    /// <param name="services">Provided service collection</param>
    /// <param name="configurationManager">App configuration defined by various sources</param>
    /// <returns><see cref="IServiceCollection"/> for further registration chaining</returns>
    public static IServiceCollection RegisterAzureDataServices(this IServiceCollection services, IConfiguration configurationManager)
    {
        services.AddResponseCompression(options =>
        {
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { MediaTypeNames.Application.Octet });
        });

        services.AddControllersWithViews();

        services.AddSignalR(options => options.MaximumReceiveMessageSize = 104_857_600);

        services.AddServerSideBlazor()
            .AddHubOptions(options =>
            {
                options.MaximumReceiveMessageSize = 104_857_600;
            })
            .AddMicrosoftIdentityConsentHandler();

        return services;
    }

    private static IServiceCollection AddIndexedDbServices(this IServiceCollection services)
    {
        services
            .AddScoped<IModuleFactory, EsModuleFactory>()
            .AddScoped<YsecIndexedDbContext>();
        
        return services;
    }
}
