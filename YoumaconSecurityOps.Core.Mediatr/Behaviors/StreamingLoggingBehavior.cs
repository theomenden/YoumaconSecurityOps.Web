namespace YoumaconSecurityOps.Core.Mediatr.Behaviors;

public class StreamingLoggingBehavior<TRequest, TResponse> : IStreamPipelineBehavior<TRequest, TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    private readonly ILogger<StreamingLoggingBehavior<TRequest, TResponse>> _logger;

    public StreamingLoggingBehavior(ILogger<StreamingLoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public IAsyncEnumerable<TResponse> Handle(TRequest request, CancellationToken cancellationToken, StreamHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Handling {requestType}", typeof(TRequest).FullName);
        
        var response = next();

        _logger.LogInformation("Handled {tResponse}", typeof(TResponse).FullName);
        
        return response;
    }
}