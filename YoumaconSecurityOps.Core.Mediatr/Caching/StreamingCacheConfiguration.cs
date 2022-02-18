namespace YoumaconSecurityOps.Core.Mediatr.Caching
{
    public abstract class StreamingCacheConfiguration<TCache, TResult> : IStreamingCacheConfiguration
    where TCache : IStreamRequest<TResult>
    {
        protected abstract CachingOptions<TCache> ConfigureCaching();

        protected virtual Action<IServiceCollection>[] ConfigureStreamingCacheInvalidation() =>
            Array.Empty<Action<IServiceCollection>>();


        public void Register(IServiceCollection services)
        {
            services.AddSingleton<StreamingCacheAccessor<TCache, TResult>>();

            var cachingConfiguration = ConfigureCaching();

            services.RegisterStreamCaching<TCache, TResult>(cachingConfiguration.AbsoluteDuration,
                cachingConfiguration.SlidingDuration, cachingConfiguration.CachePrefix,
                cachingConfiguration.KeyGenerator);

            var invalidators = ConfigureStreamingCacheInvalidation();

            foreach (var invalidator in invalidators)
            {
                invalidator(services);
            }
        }

        public Action<IServiceCollection> RegisterStreamingInvalidators<TTrigger, TTriggerResult>(
            string cachePrefix = null)
        where TTrigger : IStreamRequest<TTriggerResult>
        {
            return builder =>
                builder.RegisterStreamCachingInvalidation<TCache, TResult, TTrigger, TTriggerResult>(cachePrefix);
        }
    }
}
