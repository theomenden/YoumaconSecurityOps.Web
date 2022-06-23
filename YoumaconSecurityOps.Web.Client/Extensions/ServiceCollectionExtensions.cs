﻿namespace YoumaconSecurityOps.Web.Client.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers application defined services in our <paramref name="services"/> using established <paramref name="appSettings"/>
    /// </summary>
    /// <param name="services">Provided service collection</param>
    /// <param name="appSettings">Provided configurations</param>
    /// <returns><see cref="IServiceCollection"/> for further registration chaining</returns>
    public static IServiceCollection AddApplicationRegistrations(this IServiceCollection services, AppSettings appSettings)
    {

        services.AddFrontEndDataServices();

        services.AddIndexedDbServices();

        return services;
    }

    /// <summary>
    /// Registers Azure dependent services in our <paramref name="services"/>
    /// </summary>
    /// <param name="services">Provided service collection</param>
    /// <param name="configurationManager">App configuration defined by various sources</param>
    /// <returns><see cref="IServiceCollection"/> for further registration chaining</returns>
    public static IServiceCollection RegisterAzureDataServices(this IServiceCollection services, IConfiguration configurationManager)
    {
        var initialScopes = configurationManager.GetValue<String>("DownstreamApi:Scopes")?.Split(' ');

        services.AddResponseCompression(options =>
        {
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { MediaTypeNames.Application.Octet });
        });

        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(configurationManager.GetSection("AzureAd"))
            .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
            .AddMicrosoftGraph(configurationManager.GetSection("DownstreamApi"))
            .AddInMemoryTokenCaches();

        services.AddControllersWithViews()
            .AddMicrosoftIdentityUI();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });

        services.AddSignalR()
            .AddAzureSignalR(configurationManager["Azure:SignalR:ConnectionString"]);

        services.AddDistributedRedisCache(setUp =>
        {
            setUp.Configuration = configurationManager["CacheConnection"];
            setUp.InstanceName = "YsecRedisCache";
        });

        services.AddServerSideBlazor()
            .AddHubOptions(options =>
            {
                options.MaximumReceiveMessageSize = 104_857_600;
            })
            .AddMicrosoftIdentityConsentHandler();

        return services;
    }

    private static IServiceCollection AddFrontEndDataServices(this IServiceCollection services)
    {
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
