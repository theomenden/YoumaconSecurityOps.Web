namespace YoumaconSecurityOps.Core.Mediatr.Caching;

public class StreamingCacheAccessor<TCache, TResult> : IStreamRequest<TResult>
{
    private readonly IAppCache _appCache;

    private readonly ILogger<StreamingCacheAccessor<TCache, TResult>> _logger;

    public StreamingCacheAccessor(IAppCache appCache, ILogger<StreamingCacheAccessor<TCache, TResult>> logger)
    {
        _appCache = appCache;
        _logger = logger;
    }

    public IAsyncEnumerable<TResult> GetOrCacheItems(TCache streamQuery, Func<IAsyncEnumerable<TResult>> itemGetter,
        TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration, string keyPrefix = null, Func<TCache, string> keyGenerator = null,
        CancellationToken cancellationToken = default)
    {
        var key = streamQuery is not null ? JsonSerializer.Serialize(streamQuery) : "defaultKey";
        _logger.LogWarning("Accessing the Cache: {Prefix}:{Key}", keyPrefix, key);

        var entryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration,
            SlidingExpiration = slidingExpiration
        };

        var result = _appCache.GetOrAdd(key, () =>
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

