namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetLocationsWithParametersQueryHandler : IStreamRequestHandler<GetLocationsWithParametersQuery, LocationReader>
{
    private readonly ILocationAccessor _locations;

    private readonly IMapper _mapper;
    
    private readonly ILogger<GetLocationsWithParametersQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetLocationsWithParametersQueryHandler(ILocationAccessor locations, IMapper mapper, ILogger<GetLocationsWithParametersQueryHandler> logger, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _locations = locations;
        _mapper = mapper;
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<LocationReader> Handle(GetLocationsWithParametersQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var locations = _locations.GetAllAsync(context, cancellationToken);
        
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
}