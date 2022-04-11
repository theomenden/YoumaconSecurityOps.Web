using Microsoft.Extensions.Caching.Distributed;

namespace YoumaconSecurityOps.Core.Mediatr.DistributedCaching.Configurations;

public class GetStaffRolesCache : DistributedCache<GetStaffRolesQuery, IEnumerable<StaffRole>>
{
    protected override TimeSpan? AbsoluteExpirationRelativeToNow => new TimeSpan(0, 5, 0);

    public GetStaffRolesCache(IDistributedCache distributedCache) : base(distributedCache)
    {
    }

    protected override string GetCacheKeyIdentifier(GetStaffRolesQuery request)
    {
        return request.Issuer;
    }
}

