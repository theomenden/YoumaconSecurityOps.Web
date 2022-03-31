namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetLocationsQueryHandler : IStreamRequestHandler<GetLocationsQuery, LocationReader>
{
    private readonly ILocationAccessor _locations;
    

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetLocationsQueryHandler(ILocationAccessor locations, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _locations = locations;
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