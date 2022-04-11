using System.IO;
using MediatR.Pipeline;
using YoumaconSecurityOps.Core.Mediatr.DistributedCaching;
using YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;
using YoumaconSecurityOps.Core.Mediatr.Infrastructure;
using YoumaconSecurityOps.Core.Mediatr.Processors;

namespace YoumaconSecurityOps.Core.Mediatr.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the services needed for our Mediator implementation within the <see cref="YoumaconSecurityOps"/> application.
    /// </summary>
    /// <typeparam name="T">The base type for which we grab it's assembly</typeparam>
    /// <param name="services">The service collection</param>
    /// <returns><see cref="IServiceCollection"/> for further chaining</returns>
    public static IServiceCollection AddMediatrServices<T>(this IServiceCollection services)
    {
        var containingType = typeof(T);

        services.AddScoped<ServiceFactory>(p => p.GetService);

        services.AddMediatR(typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly);

        var writer = new WrappingWriter(Console.Out);

        services.AddSingleton<TextWriter>(writer);

        services.AddLazyCache();

        services.AddScoped(typeof(IRequestPreProcessor<>), typeof(EmptyRequestPreProcessor<>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamingLoggingBehavior<,>));
#if DEBUG
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(OperationProfilingBehaviour<,>));
        services.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamingOperationProfilingBehaviour<,>));
#endif

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DistributedCacheInvalidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DistributedCachingBehavior<,>));
        
        services.RegisterBuiltCaches(containingType);

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IMediator), containingType)
            .AddClasses()
            .AsImplementedInterfaces());

        services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(EmptyRequestPostProcessor<,>));

        return services;
    }
}