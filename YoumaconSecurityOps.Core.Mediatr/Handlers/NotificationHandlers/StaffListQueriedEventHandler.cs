namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class StaffListQueriedEventHandler: INotificationHandler<StaffListQueriedEvent>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly IMediator _mediator;

    private ILogger<StaffListQueriedEventHandler> _logger;

    public StaffListQueriedEventHandler(IEventStoreRepository eventStore, IMediator mediator, ILogger<StaffListQueriedEventHandler> logger)
    {
        _eventStore = eventStore;
        _mediator = mediator;
        _logger = logger;
    }
      

    public async Task Handle(StaffListQueriedEvent notification, CancellationToken cancellationToken)
    {
        var @event = new EventReader
        {
            Aggregate = notification.Aggregate,
            Data = notification.DataAsJson,
            MajorVersion = notification.MajorVersion,
            MinorVersion = notification.MinorVersion,
            Name = notification.Name
        };
            
        var previousEventsOnAggregate = new List<EventReader> {@event};

        await _eventStore.SaveAsync(notification.Id, notification.MinorVersion, previousEventsOnAggregate.ToList().AsReadOnly(),
            notification.Aggregate, cancellationToken);
    }
}