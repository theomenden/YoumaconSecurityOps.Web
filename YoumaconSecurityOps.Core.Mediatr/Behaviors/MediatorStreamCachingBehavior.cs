namespace YoumaconSecurityOps.Core.Mediatr.Behaviors;

public class MediatorStreamCachingBehavior<TCache, TResult> : IStreamPipelineBehavior<TCache, TResult>
        where TCache : IStreamRequest<TResult>
{
    private readonly TimeSpan? _absoluteExpiration;

    private readonly TimeSpan? _slidingExpiration;

    private readonly string _keyPrefix;

    private readonly Func<TCache, string> _keyGenerator;

    private readonly StreamingCacheAccessor<TCache, TResult> _cacheAccessor;

    public MediatorStreamCachingBehavior(StreamingCacheAccessor<TCache, TResult> cacheAccessor, TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration, string keyPrefix = null, Func<TCache, string> keyGenerator = null)
    {
        _absoluteExpiration = absoluteExpiration;
        _slidingExpiration = slidingExpiration;
        _keyPrefix = string.IsNullOrWhiteSpace(keyPrefix) ? typeof(TCache).GetTypeInfo().FullName ?? nameof(TCache) : keyPrefix;
        _keyGenerator = keyGenerator ?? (cache => string.Empty);
        _cacheAccessor = cacheAccessor;
    }

    public async IAsyncEnumerable<TResult> Handle(TCache request, [EnumeratorCancellation] CancellationToken cancellationToken, StreamHandlerDelegate<TResult> next)
    {
        await foreach (var item in next().WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            var result = await _cacheAccessor.GetOrCacheItems(request, () => item, _absoluteExpiration, _slidingExpiration, _keyPrefix, _keyGenerator, cancellationToken);

            yield return result;
        }

    }
}
