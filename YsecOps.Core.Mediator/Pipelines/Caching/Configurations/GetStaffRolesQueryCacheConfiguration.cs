namespace YsecOps.Core.Mediator.Pipelines.Caching.Configurations;
internal sealed class GetStaffRolesQueryCacheConfiguration : CacheConfiguration<GetStaffRolesQuery, List<Role>>
{
    protected override CachingOptions<GetStaffRolesQuery> ConfigureCaching()
    {
        return new()
        {
            AbsoluteDuration = TimeSpan.FromHours(10),
            SlidingDuration = TimeSpan.FromHours(1)
        };
    }
}
