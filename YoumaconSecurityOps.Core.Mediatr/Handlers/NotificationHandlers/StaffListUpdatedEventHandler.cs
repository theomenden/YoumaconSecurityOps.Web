namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal class StaffListUpdatedEventHandler: INotificationHandler<StaffListUpdatedEvent>
{
    private readonly IEventStoreRepository _eventStore;
    
    private readonly IDbContextFactory<EventStoreDbContext> _dbContextFactory;

    public StaffListUpdatedEventHandler(IEventStoreRepository eventStore, IDbContextFactory<EventStoreDbContext> dbContextFactory)
    {
        _eventStore = eventStore;
        _dbContextFactory = dbContextFactory;
    }

    public async Task Handle(StaffListUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var previousEvents = (await _eventStore.GetAllByAggregateIdAsync(context, notification.AggregateId, cancellationToken))
            .ToList();

        await _eventStore.SaveAsync(context, notification.AggregateId, notification.MinorVersion,
            nameof(StaffListUpdatedEvent), previousEvents, notification.Aggregate, cancellationToken);
    }
}

