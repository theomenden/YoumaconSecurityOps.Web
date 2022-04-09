namespace YoumaconSecurityOps.Core.Mediatr.DistributedCaching;

/// <summary>
/// Contains methods relating to caching - these are wrappers to make using the distributed cache api more intuitive.
/// </summary>
public interface ICache<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Retrieve an item of type <see cref="TResponse"/> from the cache asynchronously
    /// </summary>
    /// <param name="request">The type of request that we want to check the cache for</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An instance of <see cref="TResponse"/></returns>
    Task<TResponse> GetAsync(TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores the <typeparamref name="TResponse" /> in the cache with a key based off of the <typeparamref name="TRequest"/>
    /// </summary>
    /// <param name="request">The incoming request that we want to cache the response from</param>
    /// <param name="response">The response that we want to cache</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetAsync(TRequest request, TResponse response, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the response from the cache using the CacheKeyIdentifier from the Request
    /// </summary>
    /// <param name="cacheKeyIdentifier">A string identifier that uniquely identifies the response to be removed</param>
    /// <returns></returns>
    Task RemoveAsync(String cacheKeyIdentifier, CancellationToken cancellationToken = default);
}

