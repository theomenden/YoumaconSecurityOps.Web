namespace YoumaconSecurityOps.Core.Mediatr.Caching
{
    public abstract class CacheConfiguration<TCache, TResult> : ICacheConfiguration
    where TCache : IRequest<TResult>
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

            foreach (var invalidator in invalidators)
            {
                invalidator(services);
            }
        }

        public Action<IServiceCollection> RegisterInvalidator<TTrigger, TTriggerResult>(string cachePrefix = null)
        where TTrigger : IRequest<TTriggerResult>
        {
            return builder =>
                builder.RegisterCachingInvalidation<TCache, TResult, TTrigger, TTriggerResult>(cachePrefix);
        }

        public Action<IServiceCollection> RegisterInvalidatorFromRequest<TTrigger>(string cachePrefix = null)
            where TTrigger : IRequest
        {
            return builder =>
                builder.RegisterCachingInvalidationForRequest<TCache, TResult, TTrigger>(cachePrefix);
        }
    }
}
