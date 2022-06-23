using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YSecOps.Events.EfCore.Contexts;

namespace YSecOps.Events.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
#if DEBUG
    private static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });
#endif

    public static IServiceCollection AddYSecEventStoreServices(this IServiceCollection services,
        string ysecEventStoreConnection)
    {
        services.AddPooledDbContextFactory<YSecOpsEventStoreContext>(options =>
            options.EnableServiceProviderCaching()
#if DEBUG
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .UseLoggerFactory(LoggerFactory)
#endif
                .UseSqlServer(ysecEventStoreConnection)
        );

        return services;
    }
}