namespace YoumaconSecurityOps.Core.Mediatr.Caching.Configurations.Streaming;

    public class GetStaffTypesQueryStreamingCache : StreamingCacheConfiguration<GetStaffTypesQuery, StaffType>
    {
        protected override CachingOptions<GetStaffTypesQuery> ConfigureCaching()
        {
            return new CachingOptions<GetStaffTypesQuery>
            {
                AbsoluteDuration = TimeSpan.FromHours(10),
                SlidingDuration = TimeSpan.FromHours(1)
            };
        }

        protected override Action<IServiceCollection>[] ConfigureStreamingCacheInvalidation()
        {
            return new[]
            {
                RegisterStreamingInvalidators<GetStaffTypesQuery, StaffType>()
            };
        }
    }

