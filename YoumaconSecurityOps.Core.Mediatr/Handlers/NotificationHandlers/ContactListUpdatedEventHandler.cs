namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ContactListUpdatedEventHandler: INotificationHandler<ContactListUpdatedEvent>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly IDbContextFactory<EventStoreDbContext> _dbContextFactory;

    public ContactListUpdatedEventHandler(IEventStoreRepository eventStore, IDbContextFactory<EventStoreDbContext> dbContextFactory)
    {
        _eventStore = eventStore;
        _dbContextFactory = dbContextFactory;
    }

    public async Task Handle(ContactListUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var previousEvents =(await _eventStore.GetAllByAggregateIdAsync(context, notification.AggregateId, cancellationToken)).ToList();

        await _eventStore.SaveAsync(context, notification.AggregateId, notification.MinorVersion,
            nameof(ContactListUpdatedEvent), previousEvents, notification.Aggregate, cancellationToken);
    }
}