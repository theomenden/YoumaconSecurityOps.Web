namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ShiftUpdatedEventHandler: INotificationHandler<ShiftUpdatedEvent>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly ILogger<ShiftUpdatedEventHandler> _logger;

    public ShiftUpdatedEventHandler(IEventStoreRepository eventStore, ILogger<ShiftUpdatedEventHandler> logger)
    {
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task Handle(ShiftUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var previousEvents =  (await _eventStore.GetAllByAggregateIdAsync(notification.Id, cancellationToken)).ToList();

        await _eventStore.SaveAsync(notification.Id, notification.MajorVersion, previousEvents.AsReadOnly(), notification.Name, cancellationToken);
    }
}