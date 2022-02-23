namespace YoumaconSecurityOps.Core.Mediatr.Caching.Configurations.Streaming;

    public class GetStaffRolesQueryStreamingCache : StreamingCacheConfiguration<GetStaffRolesQuery, StaffRole>
    {
        protected override CachingOptions<GetStaffRolesQuery> ConfigureCaching()
        {
            return new CachingOptions<GetStaffRolesQuery>()
            {
                AbsoluteDuration = TimeSpan.FromHours(10),
                SlidingDuration = TimeSpan.FromHours(1)
            };
        }

        protected override Action<IServiceCollection>[] ConfigureStreamingCacheInvalidation()
        {
            return new[]
            {
                RegisterStreamingInvalidators<GetStaffRolesQuery, StaffRole>()
            };
        }
    }

