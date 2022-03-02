namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetEventListQueryHandler : IStreamRequestHandler<GetEventListQuery, EventReader>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreContextFactory;

    private readonly IEventStoreRepository _eventStore;
        
    private readonly ILogger<GetEventListQueryHandler> _logger;

    public GetEventListQueryHandler(IDbContextFactory<EventStoreDbContext> eventStoreContextFactory, IEventStoreRepository eventStore, ILogger<GetEventListQueryHandler> logger)
    {
        _eventStoreContextFactory = eventStoreContextFactory;
        _eventStore = eventStore;
        _logger = logger;
    }


    public async IAsyncEnumerable<EventReader> Handle(GetEventListQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context =
            await _eventStoreContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        await foreach (var @event in _eventStore.GetAll(context, cancellationToken))
        {
            yield return @event;
        }
    }
}