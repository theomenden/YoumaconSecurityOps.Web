namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetLocationsQueryHandler : IStreamRequestHandler<GetLocationsQuery, LocationReader>
{
    private readonly ILocationAccessor _locations;

    private readonly ILogger<GetLocationsQueryHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetLocationsQueryHandler(ILocationAccessor locations, ILogger<GetLocationsQueryHandler> logger, IMapper mapper, IMediator mediator, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _locations = locations;
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
        _dbContextFactory = dbContextFactory;   
    }

    public async IAsyncEnumerable<LocationReader> Handle(GetLocationsQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);
        
        await foreach (var location in _locations.GetAllAsync(context, cancellationToken).ConfigureAwait(false))
        {
            yield return location;
        }
    }
}