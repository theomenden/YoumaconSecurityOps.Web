namespace YsecOps.Core.Mediator.Pipelines.Behaviors;

internal sealed class StreamProfilingBehavior<TRequest, TResponse> : IStreamPipelineBehavior<TRequest, TResponse>
where TRequest : IStreamRequest<TResponse>
{
    private readonly ILogger<StreamProfilingBehavior<TRequest, TResponse>> _logger;

    public StreamProfilingBehavior(ILogger<StreamProfilingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }


    public IAsyncEnumerable<TResponse> Handle(TRequest request, CancellationToken cancellationToken, StreamHandlerDelegate<TResponse> next)
    {
        var timer = new System.Diagnostics.Stopwatch();

        timer.Start();

        var response = next();

        timer.Stop();

        _logger.TraceMessageProfiling(timer.ElapsedMilliseconds);

        return response;
    }
}