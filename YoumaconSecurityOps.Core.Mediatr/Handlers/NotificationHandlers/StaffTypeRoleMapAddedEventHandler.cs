namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal class StaffTypeRoleMapAddedEventHandler : INotificationHandler<StaffTypeRoleMapAddedEvent>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly ILogger<StaffTypeRoleMapAddedEventHandler> _logger;

    public StaffTypeRoleMapAddedEventHandler(IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory, IEventStoreRepository eventStore, ILogger<StaffTypeRoleMapAddedEventHandler> logger)
    {
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task Handle(StaffTypeRoleMapAddedEvent notification, CancellationToken cancellationToken)
    {
        await using var context =
            await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var eventsOnThisAggregate = (await _eventStore
                .GetAllByAggregateId(context,notification.Id, cancellationToken)
                .ToListAsync(cancellationToken))
            .AsReadOnly();

        await _eventStore.SaveAsync(context,notification.Id, notification.MinorVersion, eventsOnThisAggregate, notification.Name, cancellationToken);
    }
}

