﻿namespace YsecOps.Core.Mediator.Pipelines.Caching;

public static class CachingServiceRegistrations
{
    /// <summary>
    /// Adds caching for any mediator query
    /// </summary>
    /// <typeparam name="TCache">Query we want to cache</typeparam>
    /// <typeparam name="TResult">Result of the cached query</typeparam>
    /// <param name="services">Services registered in the app</param>
    /// <param name="absoluteCacheDuration">Absolute lifetime of the cache</param>
    /// <param name="slidingCacheDuration">Sliding lifetime of the cache</param>
    /// <param name="cachePrefix">Used for invalidation. Defaults to query type namespace.name</param>
    /// <param name="keyGenerator">Defaults to json serialized query</param>
    /// <returns><see cref="IServiceCollection"/> for further chaining</returns>
    public static IServiceCollection RegisterCaching<TCache, TResult>(this IServiceCollection services,
        TimeSpan? absoluteCacheDuration, TimeSpan? slidingCacheDuration, string cachePrefix = null,
        Func<TCache, string> keyGenerator = null)
    where TCache : ICacheableQuery<TResult>
    {
        services.AddTransient<IPipelineBehavior<TCache, TResult>>(config =>
        {
            var cache = config.GetRequiredService<CacheAccessor<TCache, TResult>>();

            return new MediatorCachingBehavior<TCache, TResult>(cache, absoluteCacheDuration, slidingCacheDuration,
                cachePrefix, keyGenerator);
        });

        return services;
    }

    /// <summary>
    /// Adds a cache invalidation trigger for query to query.
    /// </summary>
    /// <typeparam name="TCache">Query we want to invalidate</typeparam>
    /// <typeparam name="TCacheResult">Result of the invalidated query</typeparam>
    /// <typeparam name="TTrigger">Query to trigger invalidation</typeparam>
    /// <typeparam name="TTriggerResult">Result of query to trigger invalidation</typeparam>
    /// <param name="services">Services registered in the app</param>
    /// <param name="cachePrefix">Used for invalidation. Defaults to query type namespace.name</param>
    /// <returns><see cref="IServiceCollection"/> for further chaining</returns>
    public static IServiceCollection RegisterCachingInvalidation<TCache, TCacheResult, TTrigger, TTriggerResult>(
        this IServiceCollection services,
        string cachePrefix = null)
        where TCache : ICacheableQuery<TCacheResult>
        where TTrigger : IRequest<TTriggerResult>
    {
        services.AddTransient<IPipelineBehavior<TTrigger, TTriggerResult>>(config =>
        {
            var cache = config.GetRequiredService<CacheAccessor<TCache, TCacheResult>>();

            var logger = config.GetRequiredService<ILogger<CacheAccessor<TCache, TCacheResult>>>();

            return new MediatorCachingInvalidationBehavior<TCache, TCacheResult, TTrigger, TTriggerResult>(cache, logger,
                cachePrefix);
        });

        return services;
    }

    /// <summary>
    /// Adds a cache invalidation trigger for query to query.
    /// </summary>
    /// <typeparam name="TCache">Query we want to invalidate</typeparam>
    /// <typeparam name="TCacheResult">Result of the invalidated query</typeparam>
    /// <typeparam name="TTrigger">Query to trigger invalidation</typeparam>
    /// <typeparam name="TTriggerResult">Result of query to trigger invalidation</typeparam>
    /// <param name="services">Services registered in the app</param>
    /// <param name="cachePrefix">Used for invalidation. Defaults to query type namespace.name</param>
    /// <returns><see cref="IServiceCollection"/> for further chaining</returns>
    public static IServiceCollection RegisterCachingInvalidationForRequest<TCache, TCacheResult, TTrigger>(
        this IServiceCollection services,
        string cachePrefix = null)
        where TCache : ICacheableQuery<TCacheResult>
        where TTrigger : IRequest
    {
        services.AddTransient<IRequestPreProcessor<TTrigger>>(config =>
        {
            var cache = config.GetRequiredService<CacheAccessor<TCache, TCacheResult>>();

            return new MediatorCacheInvalidationPreProcessor<TTrigger, TCache, TCacheResult>(cache, cachePrefix);
        });

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterBuiltCaches<T>(this IServiceCollection services)
    {
        var cacheConfigurations = typeof(T)
            .Assembly
            .GetTypes();

        if (!cacheConfigurations.Any(t => !t.IsGenericType && t.GetInterfaces().Contains(typeof(ICacheConfiguration))))
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            cacheConfigurations = executingAssembly.GetTypes();
        }

        cacheConfigurations = cacheConfigurations
            .Where(t => !t.IsGenericType && t.GetInterfaces().Contains(typeof(ICacheConfiguration)))
            .ToArray();

        foreach (var configuration in cacheConfigurations)
        {
            var cacheConfiguration = Activator.CreateInstance(configuration) as ICacheConfiguration;

            cacheConfiguration.Register(services);
        }
    }
}
