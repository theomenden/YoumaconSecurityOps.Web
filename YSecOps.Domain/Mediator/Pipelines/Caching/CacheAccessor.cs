using YsecOps.Core.Mediator.Infrastructure;

namespace YsecOps.Core.Mediator.Pipelines.Caching;

public class CacheAccessor<TCache, TResult> : ICacheableQuery<TResult>
{
    private readonly IAppCache _appCache;

    private readonly ILogger<CacheAccessor<TCache, TResult>> _logger;

    public CacheAccessor(IAppCache appCache, ILogger<CacheAccessor<TCache, TResult>> logger)
    {
        _appCache = appCache;
        _logger = logger;
    }

    public async Task<TResult> GetOrCacheItem(TCache query, Func<Task<TResult>> itemGetter,
        TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration, string keyPrefix = null, Func<TCache, string> keyGenerator = null)
    {
        var key = query is not null ? JsonSerializer.Serialize(query) : "defaultKey";
        _logger.LogWarning("Accessing the Cache: {Prefix}:{Key}", keyPrefix, key);

        var entryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration,
            SlidingExpiration = slidingExpiration
        };

        var result = await _appCache.GetOrAddAsync(key, () =>
        {
            //updates our partial key
            var partials = _appCache.GetOrAdd(keyPrefix, _ => new List<string>());

            if (!partials.Contains(key))
            {
                partials.Add(key);
                _appCache.Add(keyPrefix, partials, new MemoryCacheEntryOptions { Priority = CacheItemPriority.NeverRemove });
            }
            _logger.LogWarning("Caching Mediator: {Prefix}:{Key}", keyPrefix, key);

            return itemGetter();
        }, entryOptions);

        return result;
    }

    public int RemoveItemFromCache(string keyPrefix)
    {
        _logger.LogWarning("Invalidating Cache for : {Prefix}", keyPrefix);

        var qualifiedKeyList = _appCache.Get<List<string>>(keyPrefix) ?? new List<string>();

        var qualifiedKeyCount = qualifiedKeyList.Count;

        foreach (var key in qualifiedKeyList)
        {
            _appCache.Remove(key);
        }

        _appCache.Remove(keyPrefix);

        return qualifiedKeyCount;
    }
}