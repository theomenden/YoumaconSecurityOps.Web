
namespace YoumaconSecurityOps.Core.Mediatr.Caching.Configurations.Request;

    public class GetStaffRolesQueryCache : CacheConfiguration<GetStaffRolesQuery, IEnumerable<StaffRole>>
    {
        protected override CachingOptions<GetStaffRolesQuery> ConfigureCaching()
        {
            return new CachingOptions<GetStaffRolesQuery>
            {
                AbsoluteDuration = TimeSpan.FromHours(10),
                SlidingDuration = TimeSpan.FromHours(1)
            };
        }
    }