

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class LocationCreatedEventHandler: INotificationHandler<LocationCreatedEvent>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    private readonly ILocationRepository _locations;

    private readonly IMediator _mediator;

    private readonly ILogger<LocationCreatedEventHandler> _logger;

    public LocationCreatedEventHandler(IEventStoreRepository eventStore, IMapper mapper,ILocationRepository locations, IMediator mediator, ILogger<LocationCreatedEventHandler> logger)
    {
        _eventStore = eventStore;
        _mapper = mapper;
        _locations = locations;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(LocationCreatedEvent notification, CancellationToken cancellationToken)
    {
        var locationEntry = _mapper.Map<LocationReader>(notification.LocationAdded);

        await _locations.AddAsync(locationEntry, cancellationToken);

        await RaiseLocationAddedEvent(notification, locationEntry, cancellationToken);
    }

    private async Task RaiseLocationAddedEvent(LocationCreatedEvent createdLocation, LocationReader locationAdded, CancellationToken cancellationToken)
    {
        var e = new LocationAddedEvent(locationAdded)
        {
            Aggregate = createdLocation.Aggregate,
            DataAsJson = locationAdded.ToJson(),
            MajorVersion = createdLocation.MajorVersion,
            MinorVersion = ++createdLocation.MinorVersion,
            Name = createdLocation.Name
        };

        await _mediator.Publish(e, cancellationToken);
    }
}