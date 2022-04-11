using Microsoft.Extensions.Caching.Distributed;

namespace YoumaconSecurityOps.Core.Mediatr.DistributedCaching.Configurations;
public class GetLocationsCache : DistributedCache<GetLocationsQuery, IEnumerable<LocationReader>>
{
    protected override TimeSpan? AbsoluteExpirationRelativeToNow => new TimeSpan(0, 5, 0);

    public GetLocationsCache(IDistributedCache distributedCache) : base(distributedCache)
    {
    }

    protected override string GetCacheKeyIdentifier(GetLocationsQuery request)
    {
        return request.Issuer;
    }
}