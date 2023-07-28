namespace YsecOps.Core.Mediator.Pipelines.Caching.Configurations;

internal sealed class GetStaffTypesQueryCacheConfiguration : CacheConfiguration<GetStaffTypesQuery, List<StaffType>>
{
    protected override CachingOptions<GetStaffTypesQuery> ConfigureCaching()
    {
        return new()
        {
            AbsoluteDuration = TimeSpan.FromHours(10),
            SlidingDuration = TimeSpan.FromHours(1)
        };
    }
}
