namespace YsecOps.Core.Mediator.Pipelines.Behaviors;

internal sealed class OperationProfilingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly ILogger<OperationProfilingBehavior<TRequest, TResponse>> _logger;

    public OperationProfilingBehavior(ILogger<OperationProfilingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var timer = new System.Diagnostics.Stopwatch();

        timer.Start();
        
        var response = next();

        timer.Stop();

        _logger.TraceMessageProfiling(timer.ElapsedMilliseconds);

        return response;
    }
}