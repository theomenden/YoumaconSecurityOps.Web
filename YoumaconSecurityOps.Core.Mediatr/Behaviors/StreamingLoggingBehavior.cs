namespace YoumaconSecurityOps.Core.Mediatr.Behaviors;

public class StreamingLoggingBehavior<TRequest, TResponse> : IStreamPipelineBehavior<TRequest, TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    private readonly ILogger<StreamingLoggingBehavior<TRequest, TResponse>> _logger;

    public StreamingLoggingBehavior(ILogger<StreamingLoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async IAsyncEnumerable<TResponse> Handle(TRequest request, [EnumeratorCancellation] CancellationToken cancellationToken, StreamHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Handling {requestType}", typeof(TRequest).FullName);

        _logger.LogInformation("Request Contents: {@request}", request);

        await foreach (var response in next().WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return response;
            _logger.LogInformation("Handled {tResponse}", typeof(TResponse).FullName);
            _logger.LogInformation("Response Contents {@response}", response);
        }
    }
}