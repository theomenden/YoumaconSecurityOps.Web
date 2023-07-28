namespace YsecOps.Core.Mediator.Pipelines.Caching;

public abstract class CacheConfiguration<TCache, TResult> : ICacheConfiguration
    where TCache : ICacheableQuery<TResult>
{
    protected abstract CachingOptions<TCache> ConfigureCaching();

    protected virtual Action<IServiceCollection>[] ConfigureCacheInvalidation() =>
        Array.Empty<Action<IServiceCollection>>();
    
    public void Register(IServiceCollection services)
    {
        services.AddSingleton<CacheAccessor<TCache, TResult>>();

        var cachingConfiguration = ConfigureCaching();

        services.RegisterCaching<TCache, TResult>(cachingConfiguration.AbsoluteDuration,
            cachingConfiguration.SlidingDuration, cachingConfiguration.CachePrefix,
            cachingConfiguration.KeyGenerator);

        var invalidators = ConfigureCacheInvalidation();

        Array.ForEach(invalidators, invalidator => invalidator(services));
    }

    protected Action<IServiceCollection> RegisterInvalidator<TTrigger, TTriggerResult>(string cachePrefix = null)
        where TTrigger : IRequest<TTriggerResult>
    {
        return builder =>
            builder.RegisterCachingInvalidation<TCache, TResult, TTrigger, TTriggerResult>(cachePrefix);
    }

    protected Action<IServiceCollection> RegisterInvalidatorFromRequest<TTrigger>(string cachePrefix = null)
        where TTrigger : IRequest
    {
        return builder =>
            builder.RegisterCachingInvalidationForRequest<TCache, TResult, TTrigger>(cachePrefix);
    }
}