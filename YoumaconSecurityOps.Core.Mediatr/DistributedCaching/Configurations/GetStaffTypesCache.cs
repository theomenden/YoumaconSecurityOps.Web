using Microsoft.Extensions.Caching.Distributed;

namespace YoumaconSecurityOps.Core.Mediatr.DistributedCaching.Configurations;

public class GetStaffTypesCache : DistributedCache<GetStaffTypesQuery, IEnumerable<StaffType>>
{
    protected override TimeSpan? AbsoluteExpirationRelativeToNow => new TimeSpan(0, 5, 0);

    public GetStaffTypesCache(IDistributedCache distributedCache) : base(distributedCache)
    {
    }

    protected override string GetCacheKeyIdentifier(GetStaffTypesQuery request)
    {
        return request.Issuer;
    }
}
