namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class LocationRepository: ILocationAccessor, ILocationRepository
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    public LocationRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentException("Could not register DbContext: ", nameof(dbContext));
    }

    public IAsyncEnumerable<LocationReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var locations = dbContext.Locations
            .AsAsyncEnumerable()
            .OrderBy(l => l.Name);

        return locations;
    }
        
    public async Task<LocationReader> WithIdAsync(YoumaconSecurityDbContext dbContext, Guid entityId, CancellationToken cancellationToken = new())
    {
        var location = await dbContext.Locations.AsQueryable().SingleOrDefaultAsync(l => l.Id == entityId, cancellationToken);

        return location;
    }

    public IAsyncEnumerable<LocationReader> GetHotels(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var hotels = GetAllAsync(dbContext,cancellationToken)
            .Where(l => l.IsHotel)
            .OrderBy(l => l.Name);

        return hotels;
    }

    public IAsyncEnumerator<LocationReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
    {
        using var context = _dbContext.CreateDbContext();

        var locationAsyncEnumerator = GetAllAsync(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return locationAsyncEnumerator;
    }

    public async Task<bool> AddAsync(LocationReader entity, CancellationToken cancellationToken = new())
    {
        try
        {
            await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

            context.Locations.Add(entity);

            await context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}