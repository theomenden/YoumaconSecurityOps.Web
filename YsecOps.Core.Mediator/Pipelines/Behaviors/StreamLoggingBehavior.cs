namespace YsecOps.Core.Mediator.Pipelines.Behaviors;

internal sealed class StreamLoggingBehavior<TRequest, TResponse>: IStreamPipelineBehavior<TRequest, TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    private readonly ILogger<StreamLoggingBehavior<TRequest, TResponse>> _logger;

    public StreamLoggingBehavior(ILogger<StreamLoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public IAsyncEnumerable<TResponse> Handle(TRequest request, CancellationToken cancellationToken, StreamHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Starting Request processing for {trequest}", nameof(TRequest));

        var response = next();

        _logger.LogInformation("Finishing Request processing for {trequest}, giving a type {tresponse}", nameof(TRequest), nameof(TResponse));

        return response;
    }
}