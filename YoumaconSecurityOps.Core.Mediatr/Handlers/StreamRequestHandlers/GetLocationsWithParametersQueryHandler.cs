namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetLocationsWithParametersQueryHandler : IStreamRequestHandler<GetLocationsWithParametersQuery, LocationReader>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly ILocationAccessor _locations;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<GetLocationsWithParametersQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;
    public GetLocationsWithParametersQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IEventStoreRepository eventStore, ILocationAccessor locations, ILogger<GetLocationsWithParametersQueryHandler> logger, IMapper mapper, IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _eventStore = eventStore;
        _locations = locations;
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    public async IAsyncEnumerable<LocationReader> Handle(GetLocationsWithParametersQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var locations = _locations.GetAllAsync(context, cancellationToken);

        await RaiseLocationListQueriedEvent(request.Parameters, cancellationToken).ConfigureAwait(false);

        await foreach (var location in Filter(locations, request.Parameters).WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
        {
            yield return location;
        }
    }

    private static IAsyncEnumerable<LocationReader> Filter(IAsyncEnumerable<LocationReader> locations,
        LocationQueryStringParameters parameters)
    {
        var (name, isHotel) = parameters;

        if (!String.IsNullOrWhiteSpace(name))
        {
            locations = locations.Where(l => l.Name.Equals(name));
        }

        return locations.Where(l => l.IsHotel == isHotel);
    }

    private async Task RaiseLocationListQueriedEvent(LocationQueryStringParameters parameters, CancellationToken cancellation)
    {
        var e = new LocationListQueriedEvent(parameters);

        await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellation)
            .ConfigureAwait(false);

        await _mediator.Publish(e, cancellation)
            .ConfigureAwait(false);
    }
}