using System.Diagnostics;

namespace YoumaconSecurityOps.Core.Mediatr.Behaviors;

    public class OperationProfilingBehaviour<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<OperationProfilingBehaviour<TRequest, TResponse>> _logger;

        public OperationProfilingBehaviour(ILogger<OperationProfilingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopwatch = Stopwatch.StartNew();

            var result = await next();

            stopwatch.Stop();

            _logger.TraceMessageProfiling(stopwatch.ElapsedMilliseconds);

            return result;
        }
    }

