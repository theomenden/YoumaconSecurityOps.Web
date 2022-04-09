using Microsoft.Extensions.Caching.Distributed;

namespace YoumaconSecurityOps.Core.Mediatr.DistributedCaching;

/// <summary>
/// Implement this class when we want our cached request response to be stored in the distributed cache
/// </summary>
/// <typeparam name="TRequest">The type of the request who's response we want to cache</typeparam>
/// <typeparam name="TResponse">The type of the response of the request that will be cached</typeparam>
public abstract class DistributedCache<TRequest, TResponse> : ICache<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly IDistributedCache _distributedCache;

    protected virtual DateTime? AbsoluteExpiration { get; }

    protected virtual TimeSpan? AbsoluteExpirationRelativeToNow { get; }

    protected virtual TimeSpan? SlidingExpiration { get; }

    protected DistributedCache(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    /// <summary>
    /// This should be overriden to return a string key that will uniquely identify the cached response.
    /// </summary>
    /// <param name="request">The type of the request who's response will be cached</param>
    /// <returns></returns>
    protected abstract string GetCacheKeyIdentifier(TRequest request);

    private static string GetCacheKey(String id)
    {
        return $"{typeof(TRequest).FullName}:{id}";
    }

    private string GetCacheKey(TRequest request)
    {
        return GetCacheKey(GetCacheKeyIdentifier(request));
    }

    public virtual async Task<TResponse> GetAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _distributedCache.GetAsync<TResponse>(GetCacheKey(request), cancellationToken);

        return response is not null ? response : default;
    }

    public virtual async Task SetAsync(TRequest request, TResponse response, CancellationToken cancellationToken = default)
    {
        await _distributedCache.SetAsync(GetCacheKey(request), response, new DistributedCacheEntryOptions
            {
            AbsoluteExpiration = AbsoluteExpiration,
            AbsoluteExpirationRelativeToNow = AbsoluteExpirationRelativeToNow,
            SlidingExpiration = SlidingExpiration
            },
            cancellationToken);
    }

    public virtual async Task RemoveAsync(string cacheKeyIdentifier, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(GetCacheKey(cacheKeyIdentifier), cancellationToken);
    }
}

