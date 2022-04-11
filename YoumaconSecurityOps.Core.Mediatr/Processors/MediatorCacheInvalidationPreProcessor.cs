using MediatR.Pipeline;

namespace YoumaconSecurityOps.Core.Mediatr.Processors;

public class MediatorCacheInvalidationPreProcessor<TRequest, TCache, TCacheResult> : IRequestPreProcessor<TRequest>
    where TCache : IRequest<TCacheResult>
    where TRequest : IRequest
{
    private readonly CacheAccessor<TCache, TCacheResult> _cache;

    
    private readonly string _keyPrefix;

    public MediatorCacheInvalidationPreProcessor(CacheAccessor<TCache, TCacheResult> cache, string keyPrefix)
    {
        _cache = cache;
        _keyPrefix = keyPrefix;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _cache.RemoveItemFromCache(_keyPrefix);

        return Task.CompletedTask;
    }
}
