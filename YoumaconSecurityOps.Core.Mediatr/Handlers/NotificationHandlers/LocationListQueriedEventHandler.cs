namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class LocationListQueriedEventHandler: INotificationHandler<LocationListQueriedEvent>
{
    private readonly IEventStoreRepository _eventStore;


    private readonly ILogger<LocationListQueriedEventHandler> _logger;

    public LocationListQueriedEventHandler(IEventStoreRepository eventStore, ILogger<LocationListQueriedEventHandler> logger)
    {
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task Handle(LocationListQueriedEvent notification, CancellationToken cancellationToken)
    {
        var previousEvents = (await _eventStore.GetAllByAggregateId(notification.Id, cancellationToken)
            .ToListAsync(cancellationToken)).AsReadOnly();

        await _eventStore.SaveAsync(notification.Id, notification.MinorVersion, previousEvents,
            notification.Aggregate, cancellationToken);
    }
}