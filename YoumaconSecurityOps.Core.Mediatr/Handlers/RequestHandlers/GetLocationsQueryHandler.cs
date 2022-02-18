namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetLocationsQueryHandler : IStreamRequestHandler<GetLocationsQuery, LocationReader>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly ILocationAccessor _locations;

    private readonly ILogger<GetLocationsQueryHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    public GetLocationsQueryHandler(IEventStoreRepository eventStore, ILocationAccessor locations, ILogger<GetLocationsQueryHandler> logger, IMapper mapper, IMediator mediator)
    {
        _eventStore = eventStore;
        _logger = logger;
        _locations = locations;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async IAsyncEnumerable<LocationReader> Handle(GetLocationsQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await RaiseLocationsQueriedEvent(request, cancellationToken);

        await foreach (var location in _locations.GetAll(cancellationToken).ConfigureAwait(false))
        {
            yield return location;
        }
    }

    private async Task RaiseLocationsQueriedEvent(GetLocationsQuery locationQueryRequest, CancellationToken cancellationToken)
    {

        var e = new LocationListQueriedEvent(null)
        {
            Aggregate = nameof(GetLocationsQuery),
            MajorVersion = 1,
            Name = nameof(locationQueryRequest)
        };

        _logger.LogInformation("Logged event of type {LocationListQueriedEvent} {e}, {request}, {RaiseLocationsQueriedEvent}", typeof(LocationListQueriedEvent), e, locationQueryRequest, nameof(RaiseLocationsQueriedEvent));

        await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }
}