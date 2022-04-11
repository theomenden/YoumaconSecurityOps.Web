namespace YoumaconSecurityOps.Core.Mediatr.Caching.Configurations.Request;

public class GetStaffTypesQueryCache : CacheConfiguration<GetStaffTypesQuery, IEnumerable<StaffType>>
{
    protected override CachingOptions<GetStaffTypesQuery> ConfigureCaching()
    {
        return new CachingOptions<GetStaffTypesQuery>
        {
            AbsoluteDuration = TimeSpan.FromHours(10),
            SlidingDuration = TimeSpan.FromHours(1)
        };
    }
}