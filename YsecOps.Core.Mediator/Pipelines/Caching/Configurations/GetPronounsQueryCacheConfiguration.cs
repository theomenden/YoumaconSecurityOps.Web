namespace YsecOps.Core.Mediator.Pipelines.Caching.Configurations;

internal class GetPronounsQueryCacheConfiguration : CacheConfiguration<GetPronounsQuery, List<Pronoun>>
{
    protected override CachingOptions<GetPronounsQuery> ConfigureCaching()
    {
        return new CachingOptions<GetPronounsQuery>
        {
            AbsoluteDuration = TimeSpan.FromHours(10),
            SlidingDuration = TimeSpan.FromHours(1)
        };
    }

    protected override Action<IServiceCollection>[] ConfigureCacheInvalidation()
    {
        return new[] { RegisterInvalidator<GetPronounsQuery, IEnumerable<Pronoun>>() };
    }
}