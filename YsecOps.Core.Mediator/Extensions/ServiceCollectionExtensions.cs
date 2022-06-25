using YsecOps.Core.Mediator.Pipelines.Behaviors;
using YsecOps.Core.Mediator.Pipelines.Processors;

namespace YsecOps.Core.Mediator.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddYsecMediatorServices<T>(this IServiceCollection services)
    {
        var containingType = typeof(T);

        services.AddScoped<ServiceFactory>(p => p.GetService);

        services.AddMediatR(typeof(T).GetTypeInfo().Assembly, typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly);

        services.AddLazyCache();

        services.AddScoped(typeof(IRequestPreProcessor<>), typeof(EmptyRequestPreProcessor<>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamLoggingBehavior<,>));
#if DEBUG
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(OperationProfilingBehavior<,>));
        services.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamProfilingBehavior<,>));
#endif

        //      services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DistributedCacheInvalidationBehavior<,>));
        //      services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DistributedCachingBehavior<,>));

        //services.RegisterBuiltCaches(containingType);

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IMediator), containingType)
            .AddClasses()
            .AsImplementedInterfaces());

        services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(EmptyRequestPostProcessor<,>));

        return services;
    }
}