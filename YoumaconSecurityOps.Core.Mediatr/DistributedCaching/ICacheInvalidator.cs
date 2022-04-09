namespace YoumaconSecurityOps.Core.Mediatr.DistributedCaching;

public interface ICacheInvalidator<in TRequest>
{
    /// <summary>
    /// Removes the cached response entry for a <paramref name="request"/> identified by this request's CacheKeyIdentifier
    /// </summary>
    /// <param name="request">The type of the request that will cause another cached request to be invalidated</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task InvalidateAsync(TRequest request, CancellationToken cancellationToken = default);
}

