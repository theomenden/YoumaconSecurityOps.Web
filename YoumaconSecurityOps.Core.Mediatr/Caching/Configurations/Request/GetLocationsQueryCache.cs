namespace YoumaconSecurityOps.Core.Mediatr.Caching.Configurations.Request;

public class GetLocationsQueryCache : CacheConfiguration<GetLocationsQuery, IEnumerable<LocationReader>>
{
    protected override CachingOptions<GetLocationsQuery> ConfigureCaching()
    {
        return new CachingOptions<GetLocationsQuery>
        {
            AbsoluteDuration = TimeSpan.FromHours(10),
            SlidingDuration = TimeSpan.FromHours(1)
        };
    }

    protected override Action<IServiceCollection>[] ConfigureCacheInvalidation()
    {
        return new[]
        {
            RegisterInvalidatorFromRequest<AddLocationCommand>()
        };
    }
}