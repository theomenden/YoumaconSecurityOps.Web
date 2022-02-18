namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class IncidentListQueriedEventHandler : INotificationHandler<IncidentListQueriedEvent>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly IMediator _mediator;

    private ILogger<IncidentListQueriedEventHandler> _logger;

    public IncidentListQueriedEventHandler(IEventStoreRepository eventStore, IMediator mediator, ILogger<IncidentListQueriedEventHandler> logger)
    {
        _eventStore = eventStore;
        _mediator = mediator;
        _logger = logger;
    }


    public async Task Handle(IncidentListQueriedEvent notification, CancellationToken cancellationToken)
    {
        var @event = new EventReader
        {
            Aggregate = notification.Aggregate,
            Data = notification.DataAsJson,
            MajorVersion = notification.MajorVersion,
            MinorVersion = notification.MinorVersion,
            Name = notification.Name
        };

        var previousEventsOnAggregate = new List<EventReader> { @event };

        await _eventStore.SaveAsync(notification.Id, notification.MinorVersion, previousEventsOnAggregate.ToList().AsReadOnly(),
            notification.Aggregate, cancellationToken);
    }
}