namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class LocationAddedEventHandler: INotificationHandler<LocationAddedEvent>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IMediator _mediator;

    private readonly IEventStoreRepository _eventStore;

    private readonly ILogger<LocationAddedEventHandler> _logger;

    public LocationAddedEventHandler(IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory, IMediator mediator, IEventStoreRepository eventStore, ILogger<LocationAddedEventHandler> logger)
    {
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
        _mediator = mediator;
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task Handle(LocationAddedEvent notification, CancellationToken cancellationToken)
    {
        await using var context =
            await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var eventsOnThisAggregate = (await _eventStore
                .GetAllByAggregateId(context, notification.Id, cancellationToken)
                .ToListAsync(cancellationToken))
            .AsReadOnly();

        await _eventStore.SaveAsync(context, notification.Id,notification.MinorVersion, nameof(LocationAddedEventHandler),eventsOnThisAggregate, notification.Name, cancellationToken);

        await RaiseLocationListUpdatedEvent(notification, cancellationToken);
    }

    private async Task RaiseLocationListUpdatedEvent(LocationAddedEvent locationAdded,
        CancellationToken cancellationToken)
    {
        var e = new LocationListUpdatedEvent
        {
            Aggregate = locationAdded.Aggregate,
            DataAsJson = locationAdded.LocationAdded.ToJson(),
            MajorVersion = locationAdded.MajorVersion,
            MinorVersion = locationAdded.MinorVersion,
            Name = locationAdded.Name
        };

        await _mediator.Publish(e, cancellationToken);
    }
}