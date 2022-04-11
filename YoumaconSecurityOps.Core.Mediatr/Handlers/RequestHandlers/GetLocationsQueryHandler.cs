namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, IEnumerable<LocationReader>>
{
    private readonly ILocationAccessor _locations;
    
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetLocationsQueryHandler(ILocationAccessor locations, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _locations = locations;
        _dbContextFactory = dbContextFactory;   
    }

    public Task<IEnumerable<LocationReader>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        return _locations.GetAllLocationsAsync(context, cancellationToken);
    }
}