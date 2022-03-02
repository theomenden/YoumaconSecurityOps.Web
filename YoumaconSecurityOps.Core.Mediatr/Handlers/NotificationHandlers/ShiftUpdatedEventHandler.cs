namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ShiftUpdatedEventHandler: INotificationHandler<ShiftUpdatedEvent>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly ILogger<ShiftUpdatedEventHandler> _logger;

    public ShiftUpdatedEventHandler(IDbContextFactory<EventStoreDbContext> eventStoreContextFactory, IEventStoreRepository eventStore, ILogger<ShiftUpdatedEventHandler> logger)
    {
        _eventStoreContextFactory = eventStoreContextFactory;
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task Handle(ShiftUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context =
            await _eventStoreContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var previousEvents =  (await _eventStore.GetAllByAggregateIdAsync(context, notification.Id, cancellationToken)).ToList();

        await _eventStore.SaveAsync(context, notification.Id, notification.MajorVersion, previousEvents.AsReadOnly(), notification.Name, cancellationToken);
    }
}