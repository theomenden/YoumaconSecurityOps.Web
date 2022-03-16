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
    
    public async IAsyncEnumerable<TResponse> Handle(TRequest request, [EnumeratorCancellation] CancellationToken cancellationToken, StreamHandlerDelegate<TResponse> next)
    {
        var stopwatch = Stopwatch.StartNew();

        await foreach (var response in next().WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            _logger.TraceMessageProfiling(stopwatch.ElapsedMilliseconds);
            yield return response;
        }

        stopwatch.Stop();
    }
}
