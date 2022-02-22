namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetLocationsQueryHandler : IStreamRequestHandler<GetLocationsQuery, LocationReader>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly ILocationAccessor _locations;

    private readonly ILogger<GetLocationsQueryHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetLocationsQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IEventStoreRepository eventStore, ILocationAccessor locations, ILogger<GetLocationsQueryHandler> logger, IMapper mapper, IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _eventStore = eventStore;
        _logger = logger;
        _locations = locations;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async IAsyncEnumerable<LocationReader> Handle(GetLocationsQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        await RaiseLocationsQueriedEvent(request, cancellationToken).ConfigureAwait(false);

        await foreach (var location in _locations.GetAllAsync(context, cancellationToken).ConfigureAwait(false))
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

        await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellationToken)
            .ConfigureAwait(false);

        await _mediator.Publish(e, cancellationToken)
            .ConfigureAwait(false);
    }
}