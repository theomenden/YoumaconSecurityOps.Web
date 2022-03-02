using Microsoft.VisualBasic;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class LocationListUpdatedEventHandler: INotificationHandler<LocationListUpdatedEvent>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly ILogger<LocationListUpdatedEvent> _logger;

    private readonly IMapper _mapper;

    public LocationListUpdatedEventHandler(IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory, IEventStoreRepository eventStore, ILogger<LocationListUpdatedEvent> logger, IMapper mapper)
    {
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
        _eventStore = eventStore;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Handle(LocationListUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context =
            await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var eventToAdd = _mapper.Map<EventReader>(notification);

        var previousEvents = await _eventStore.GetAllByAggregateId(context, eventToAdd.Id, cancellationToken).ToListAsync(cancellationToken);
            
        await _eventStore.SaveAsync(context, eventToAdd.Id, eventToAdd.MinorVersion, previousEvents.AsReadOnly(), eventToAdd.Aggregate,
            cancellationToken);
    }
}