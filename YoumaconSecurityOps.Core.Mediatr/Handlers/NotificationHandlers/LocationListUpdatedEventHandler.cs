namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class LocationListUpdatedEventHandler: INotificationHandler<LocationListUpdatedEvent>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly ILogger<LocationListUpdatedEvent> _logger;

    private readonly IMapper _mapper;

    public LocationListUpdatedEventHandler(IEventStoreRepository eventStore, IMapper mapper, ILogger<LocationListUpdatedEvent> logger)
    {
        _eventStore = eventStore;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Handle(LocationListUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var eventToAdd = _mapper.Map<EventReader>(notification);

        var previousEvents = await _eventStore.GetAllByAggregateId(eventToAdd.Id, cancellationToken).ToListAsync(cancellationToken);
            
        await _eventStore.SaveAsync(eventToAdd.Id, eventToAdd.MinorVersion, previousEvents.AsReadOnly(), eventToAdd.Aggregate,
            cancellationToken);
    }
}