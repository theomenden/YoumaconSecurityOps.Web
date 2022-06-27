namespace YsecOps.Core.Mediator.Pipelines.Caching.Configurations;

internal sealed class GetLocationsQueryCache : CacheConfiguration<GetLocationsQuery, List<Location>>
{
    protected override CachingOptions<GetLocationsQuery> ConfigureCaching()
    {
        return new ()
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