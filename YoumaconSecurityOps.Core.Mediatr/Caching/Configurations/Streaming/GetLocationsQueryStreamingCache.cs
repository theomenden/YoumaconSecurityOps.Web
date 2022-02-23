namespace YoumaconSecurityOps.Core.Mediatr.Caching.Configurations.Streaming;

public class GetLocationsQueryStreamingCache : StreamingCacheConfiguration<GetLocationsQuery, LocationReader>
{
    protected override CachingOptions<GetLocationsQuery> ConfigureCaching()
    {
        return new CachingOptions<GetLocationsQuery>
        {
            AbsoluteDuration = TimeSpan.FromHours(10),
            SlidingDuration = TimeSpan.FromHours(1)
        };
    }

    protected override Action<IServiceCollection>[] ConfigureStreamingCacheInvalidation()
    {
        return new[]
        {
                RegisterStreamingInvalidators<GetLocationsQuery, LocationReader>()
        };
    }
}

