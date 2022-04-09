using YoumaconSecurityOps.Core.Mediatr.DistributedCaching;

namespace YoumaconSecurityOps.Core.Mediatr.Behaviors;

/// <summary>
/// <para>When this behavior is injected into the MediatR pipeline, it will receive a list of instances of ICache&lt;<typeparamref name="TRequest"/>, <typeparamref name="TResponse"/>&gt; instances for every class in the project that implements this interface for the given generic types</para>
/// <para>When the request pipeline runs, this behavior will first check to see if the response for this request type is already in the cache and if so, will short-circuit the request pipeline and return the cached response</para>
/// <para>If the request response is not in the cache, the next request handler in the pipeline will be called and the response cached using the logic and settings in the derived implementation of ICache&lt;<typeparamref name="TRequest"/>, <typeparamref name="TResponse"/>&gt;</para>
/// </summary>
/// <typeparam name="TRequest">The type of the request that will have it's results cached</typeparam>
/// <typeparam name="TResponse">The response of the request to be cached</typeparam>
public class DistributedCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly List<ICache<TRequest, TResponse>> _caches;

    public DistributedCachingBehavior(IEnumerable<ICache<TRequest, TResponse>> caches)
    {
        _caches = caches.ToList();
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        //It is entirely possible that an ICache for the same request could have been added more than once.
        //We really only care about the first one.
        var cacheRequest = _caches.FirstOrDefault();

        if (cacheRequest is null)
        {
            //A cache request handler implementation for this request was not found, do nothing and continue.
            return await next();
        }

        // Try to get the response out of the cache for this request
        var cachedResult = await cacheRequest.GetAsync(request, cancellationToken);

        if (cachedResult is not null)
        {
            // cached response found, return and short-circuit the pipeline
            return cachedResult;
        }

        //No cached response was found, so continue the handler pipeline and cache the result.
        var result = await next();

        await cacheRequest.SetAsync(request, result, cancellationToken);

        return result;
    }
}

