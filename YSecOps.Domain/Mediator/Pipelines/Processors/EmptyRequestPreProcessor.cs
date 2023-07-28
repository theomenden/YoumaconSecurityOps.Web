namespace YsecOps.Core.Mediator.Pipelines.Processors;
internal class EmptyRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
where TRequest : class
{
    private readonly ILogger<EmptyRequestPreProcessor<TRequest>> _logger;

    public EmptyRequestPreProcessor(ILogger<EmptyRequestPreProcessor<TRequest>> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting Processing for {request}", nameof(TRequest));

        return Task.CompletedTask;
    }
}