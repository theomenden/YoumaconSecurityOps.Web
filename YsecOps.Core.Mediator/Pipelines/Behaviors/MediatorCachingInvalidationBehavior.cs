using YsecOps.Core.Mediator.Pipelines.Caching;

namespace YsecOps.Core.Mediator.Pipelines.Behaviors;

public class MediatorCachingInvalidationBehavior<TCache, TCacheResult, TTrigger, TTriggerResult> : IPipelineBehavior<TTrigger, TTriggerResult>
    where TCache : ICacheableQuery<TCacheResult>
    where TTrigger : IRequest<TTriggerResult>
{
    private readonly CacheAccessor<TCache, TCacheResult> _cache;

    private readonly ILogger<CacheAccessor<TCache, TCacheResult>> _logger;

    private readonly string _keyPrefix;

    public MediatorCachingInvalidationBehavior(CacheAccessor<TCache, TCacheResult> cache, ILogger<CacheAccessor<TCache, TCacheResult>> logger, string cachePrefix)
    {
        _cache = cache;
        _logger = logger;
        _keyPrefix = cachePrefix ?? typeof(TCache).GetTypeInfo().FullName ?? String.Empty;
    }

    public async Task<TTriggerResult> Handle(TTrigger request, CancellationToken cancellationToken, RequestHandlerDelegate<TTriggerResult> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError("Could not move to next item: {@ex}", ex);
            throw;
        }
        finally
        {
            var qualifiedKeysCount = _cache.RemoveItemFromCache(_keyPrefix);

            _logger.LogWarning("Invalidating Cache {Cache} for trigger {Trigger} and {Count} qualified keys based on provided partial.", _keyPrefix, typeof(TTrigger).GetTypeInfo().FullName, qualifiedKeysCount);
        }
    }
}

