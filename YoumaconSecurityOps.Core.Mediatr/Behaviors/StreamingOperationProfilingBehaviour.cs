using System.Diagnostics;

namespace YoumaconSecurityOps.Core.Mediatr.Behaviors;

public class StreamingOperationProfilingBehaviour<TRequest, TResponse> : IStreamPipelineBehavior<TRequest, TResponse>
where TRequest : IStreamRequest<TResponse>
{
    private readonly ILogger<StreamingOperationProfilingBehaviour<TRequest, TResponse>> _logger;

    public StreamingOperationProfilingBehaviour(ILogger<StreamingOperationProfilingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    public IAsyncEnumerable<TResponse> Handle(TRequest request,CancellationToken cancellationToken, StreamHandlerDelegate<TResponse> next)
    {
        var stopwatch = Stopwatch.StartNew();

        var response = next();
        
        _logger.TraceMessageProfiling(stopwatch.ElapsedMilliseconds);

        stopwatch.Stop();

        return response;
    }
}
