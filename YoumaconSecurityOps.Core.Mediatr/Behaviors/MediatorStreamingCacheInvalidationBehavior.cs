namespace YoumaconSecurityOps.Core.Mediatr.Behaviors;

public class MediatorStreamingCacheInvalidationBehavior<TCache, TCacheResult, TTrigger, TTriggerResult> : IStreamPipelineBehavior<TTrigger, TTriggerResult>
    where TCache : IStreamRequest<TCacheResult>
    where TTrigger : IStreamRequest<TTriggerResult>
{
    private readonly StreamingCacheAccessor<TCache, TCacheResult> _cache;

    private readonly ILogger<StreamingCacheAccessor<TCache, TCacheResult>> _logger;

    private readonly string _keyPrefix;

    public MediatorStreamingCacheInvalidationBehavior(StreamingCacheAccessor<TCache, TCacheResult> cache, ILogger<StreamingCacheAccessor<TCache, TCacheResult>> logger, string cachePrefix)
    {
        _cache = cache;
        _logger = logger;
        _keyPrefix = cachePrefix ?? typeof(TCache).GetTypeInfo().FullName ?? "";
    }

    public async IAsyncEnumerable<TTriggerResult> Handle(TTrigger request, [EnumeratorCancellation] CancellationToken cancellationToken, StreamHandlerDelegate<TTriggerResult> next)
    {
        await foreach (var result in next().WithCancellation(cancellationToken))
        {
            yield return result;

            var qualifiedKeysCount = _cache.RemoveItemFromCache(_keyPrefix);

            _logger.LogWarning("Invalidating Cache {Cache} for trigger {Trigger} and {Count} qualified keys based on provided partial.", _keyPrefix, typeof(TTrigger).GetTypeInfo().FullName, qualifiedKeysCount);
        }
    }
}
