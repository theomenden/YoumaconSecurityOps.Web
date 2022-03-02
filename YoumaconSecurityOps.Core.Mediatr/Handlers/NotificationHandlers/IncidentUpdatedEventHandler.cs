namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class IncidentUpdatedEventHandler : INotificationHandler<IncidentUpdatedEvent>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly ILogger<IncidentUpdatedEventHandler> _logger;

    public IncidentUpdatedEventHandler(IDbContextFactory<EventStoreDbContext> eventStoreContextFactory, IEventStoreRepository eventStore, ILogger<IncidentUpdatedEventHandler> logger)
    {
        _eventStoreContextFactory = eventStoreContextFactory;
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task Handle(IncidentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context =
            await _eventStoreContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var previousEvents = (await _eventStore.GetAllByAggregateIdAsync(context,notification.Id, cancellationToken)).ToList();
        
        await _eventStore.SaveAsync(context,notification.Id, notification.MajorVersion, previousEvents.AsReadOnly(), notification.Name, cancellationToken);
    }
}