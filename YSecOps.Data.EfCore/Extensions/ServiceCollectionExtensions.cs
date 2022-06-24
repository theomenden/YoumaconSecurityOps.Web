using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace YSecOps.Data.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
#if DEBUG
    private static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });
#endif

    public static IServiceCollection AddYSecDataServices(this IServiceCollection services, String ysecOpsConnectionString)
    {
        services.AddPooledDbContextFactory<YoumaconSecurityOpsContext>(options =>
        {
            options
                .UseSqlServer(ysecOpsConnectionString)
#if DEBUG
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory)
#endif
                .EnableServiceProviderCaching();
        });

        return services;
    }
}
