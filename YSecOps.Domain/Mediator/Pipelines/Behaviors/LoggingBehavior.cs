namespace YsecOps.Core.Mediator.Pipelines.Behaviors;
internal sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("The Request Type for {request} was {requestType}", nameof(request), typeof(TRequest));

        var response = await next();

        _logger.LogInformation("The Response Type for {response} was {responseType}", nameof(response), typeof(TResponse));

        return response;
    }
}