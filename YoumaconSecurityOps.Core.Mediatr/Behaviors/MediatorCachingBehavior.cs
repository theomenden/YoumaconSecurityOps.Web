namespace YoumaconSecurityOps.Core.Mediatr.Behaviors;

public class MediatorCachingBehavior<TCache, TResult> : IPipelineBehavior<TCache, TResult>
where TCache : IRequest<TResult>
{
    private readonly TimeSpan? _absoluteExpiration;

    private readonly TimeSpan? _slidingExpiration;

    private readonly string _keyPrefix;

    private readonly Func<TCache, string> _keyGenerator;

    private readonly CacheAccessor<TCache, TResult> _cacheAccessor;

    public MediatorCachingBehavior(CacheAccessor<TCache, TResult> cacheAccessor, TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration, string keyPrefix = null, Func<TCache, string> keyGenerator = null)
    {
        _absoluteExpiration = absoluteExpiration;
        _slidingExpiration = slidingExpiration;
        _keyPrefix = string.IsNullOrWhiteSpace(keyPrefix) ? typeof(TCache).GetTypeInfo().FullName ?? nameof(TCache) : keyPrefix;
        _keyGenerator = keyGenerator ?? (cache => string.Empty);
        _cacheAccessor = cacheAccessor;
    }

    public Task<TResult> Handle(TCache request, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
    {
        return _cacheAccessor.GetOrCacheItem(request, () => next(), _absoluteExpiration, _slidingExpiration, _keyPrefix,
            _keyGenerator);
    }
}

