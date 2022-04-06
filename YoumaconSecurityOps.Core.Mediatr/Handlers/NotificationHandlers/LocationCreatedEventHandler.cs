namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class LocationCreatedEventHandler: INotificationHandler<LocationCreatedEvent>
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    private readonly ILocationRepository _locations;

    private readonly IMediator _mediator;

    public LocationCreatedEventHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IEventStoreRepository eventStore, IMapper mapper, ILocationRepository locations, IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _eventStore = eventStore;
        _mapper = mapper;
        _locations = locations;
        _mediator = mediator;
    }

    public async Task Handle(LocationCreatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var locationEntry = _mapper.Map<LocationReader>(notification.LocationAdded);

        await _locations.AddAsync(context, locationEntry, cancellationToken);

        await RaiseLocationAddedEvent(notification, locationEntry, cancellationToken);
    }

    private async Task RaiseLocationAddedEvent(LocationCreatedEvent createdLocation, LocationReader locationAdded, CancellationToken cancellationToken)
    {
        var e = new LocationAddedEvent(locationAdded)
        {
            Aggregate = createdLocation.Aggregate,
            DataAsJson = JsonSerializer.Serialize(locationAdded),
            MajorVersion = createdLocation.MajorVersion,
            MinorVersion = ++createdLocation.MinorVersion,
            Name = createdLocation.Name
        };

        await _mediator.Publish(e, cancellationToken);
    }
}