using Microsoft.Extensions.DependencyInjection;

namespace YoumaconSecurityOps.Core.Mediatr.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMediatrServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(IServiceCollectionExtensions).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddTransient(typeof(IStreamPipelineBehavior<,>), typeof(StreamingLoggingBehavior<,>));

        services.AddLazyCache();

        return services;
    }
}