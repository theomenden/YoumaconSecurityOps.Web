namespace YoumaconSecurityOps.Core.Mediatr.DistributedCaching;

/// <summary>
/// <para>Inherit from this class when it's needed to handle the case of one request type <typeparamref name="TRequest"/> invalidating the cached response <typeparamref name="TCachedResponse"/> of a different cached request <typeparamref name="TCachedRequest"/></para>
/// </summary>
/// <typeparam name="TRequest">The type of the request that will run and cause a different cached request to be invalidated</typeparam>
/// <typeparam name="TCachedRequest">The type of the request that has been cached and should be invalidated by TRequest</typeparam>
/// <typeparam name="TCachedResponse">The type of response that is cached by a TCachedRequest</typeparam>
public abstract class DistributedCacheInvalidator<TRequest, TCachedRequest, TCachedResponse> : ICacheInvalidator<TRequest>
where TCachedRequest: IRequest<TCachedResponse>
{
    private readonly ICache<TCachedRequest, TCachedResponse> _cache;

    protected DistributedCacheInvalidator(ICache<TCachedRequest, TCachedResponse> cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// Override to retrieve the cache key identifier that uniquely identifies a request's value.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected abstract string GetCacheKeyIdentifier(TRequest request);
    
    public async Task InvalidateAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(GetCacheKeyIdentifier(request), cancellationToken);
    }
}

