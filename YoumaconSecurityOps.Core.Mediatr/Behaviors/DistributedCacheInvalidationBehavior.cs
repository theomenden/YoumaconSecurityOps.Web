using YoumaconSecurityOps.Core.Mediatr.DistributedCaching;
namespace YoumaconSecurityOps.Core.Mediatr.Behaviors;

/// <summary>
/// <para>When this behavior is injected into the MediatR pipeline - it will receive a list of instance of <see cref="ICacheInvalidator{TRequest}"/> instances for every class in the project that implements this interface for the given generic types</para>
/// <para>When the request pipeline runs, the behavior will make sure that the current request runs through the pipeline and after it returns will proceed to call InvalidateAsync on any <see cref="ICacheInvalidator{TRequest}"/> instance in the list of invalidators</para>
/// </summary>
/// <typeparam name="TRequest">The type of the request that needs to invalidate other cached request responses</typeparam>
/// <typeparam name="TResponse">The response of the request that causes invalidation of other cached request responses</typeparam>
public class DistributedCacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest: IRequest<TResponse>
{
    private readonly List<ICacheInvalidator<TRequest>> _cacheInvalidators;

    public DistributedCacheInvalidationBehavior(IEnumerable<ICacheInvalidator<TRequest>> cacheInvalidators)
    {
        _cacheInvalidators = cacheInvalidators.ToList();
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        //run through the request handler pipeline for this request.
        var result = await next();

        //Loop through each of the cache invalidators for this request type and call the InvalidateAsync method
        //passing an instance of this request in order to retrieve a cache key.
        foreach (var invalidator in _cacheInvalidators)
        {
            await invalidator.InvalidateAsync(request, cancellationToken);
        }

        return result;
    }
}

