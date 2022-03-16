using System.IO;
using MediatR.Pipeline;
using YoumaconSecurityOps.Core.Mediatr.Infrastructure;
using YoumaconSecurityOps.Core.Mediatr.Processors;

namespace YoumaconSecurityOps.Core.Mediatr.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatrServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly);

        var writer = new WrappingWriter(Console.Out);

        services.AddSingleton<TextWriter>(writer);

        services.AddLazyCache();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamingLoggingBehavior<,>));

#if DEBUG
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(OperationProfilingBehaviour<,>));
        services.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamingOperationProfilingBehaviour<,>));
#endif

        services.AddScoped(typeof(IRequestPreProcessor<>), typeof(EmptyRequestPreProcessor<>));
        services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(EmptyRequestPostProcessor<,>));
        services.RegisterBuiltCaches();
        services.RegisterBuiltStreamingCaches();

        return services;
    }
}