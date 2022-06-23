using Microsoft.Extensions.Logging;

namespace YsecOps.Core.Mediator.Pipelines.Processors;

internal class EmptyRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly ILogger<EmptyRequestPostProcessor<TRequest, TResponse>> _logger;

    public EmptyRequestPostProcessor(ILogger<EmptyRequestPostProcessor<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{request} finished, returning {response}", nameof(request), nameof(response));

        return Task.CompletedTask;
    }
}