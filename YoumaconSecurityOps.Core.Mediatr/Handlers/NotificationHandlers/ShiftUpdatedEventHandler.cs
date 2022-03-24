namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ShiftUpdatedEventHandler: INotificationHandler<ShiftUpdatedEvent>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreContextFactory;

    private readonly IEventStoreRepository _eventStore;

    public ShiftUpdatedEventHandler(IDbContextFactory<EventStoreDbContext> eventStoreContextFactory, IEventStoreRepository eventStore)
    {
        _eventStoreContextFactory = eventStoreContextFactory;
        _eventStore = eventStore;
    }

    public async Task Handle(ShiftUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context =
            await _eventStoreContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var previousEvents =  (await _eventStore.GetAllByAggregateIdAsync(context, notification.AggregateId, cancellationToken)).ToList();

        await _eventStore.SaveAsync(context, notification.Id, notification.MajorVersion, nameof(ShiftUpdatedEventHandler) ,previousEvents.AsReadOnly(), notification.Name, cancellationToken);
    }
}