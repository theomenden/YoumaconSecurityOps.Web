using Microsoft.Extensions.Caching.Distributed;

namespace YoumaconSecurityOps.Core.Mediatr.DistributedCaching.Configurations;

/// <summary>
/// <para>This class is automatically processed in the pipeline by the Distributed Cache Behavior</para>
/// <para>It will cause the <see cref="IEnumerable{T}"/> response from <see cref="GetPronounsQuery"/> to be added to the registered implementation of IDistributedcache with a sliding expiration of 30 minutes</para>
/// </summary>
public class GetPronounsCache : DistributedCache<GetPronounsQuery, IEnumerable<Pronoun>>
{
    protected override TimeSpan? AbsoluteExpirationRelativeToNow => new TimeSpan(0, 5, 0);

    public GetPronounsCache(IDistributedCache distributedCache) : base(distributedCache)
    {
    }

    protected override string GetCacheKeyIdentifier(GetPronounsQuery request)
    {
        return request.Issuer;
    }
}
