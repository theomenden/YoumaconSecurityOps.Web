namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class LocationRepository: ILocationAccessor, ILocationRepository
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    public LocationRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentException("Could not register DbContext: ", nameof(dbContext));
    }

    public IAsyncEnumerable<LocationReader> GetAll(CancellationToken cancellationToken = new())
    {
        using var context = _dbContext.CreateDbContext();

        var locations = context.Locations
            .AsAsyncEnumerable()
            .OrderBy(l => l.Name);

        return locations;
    }
        
    public async Task<LocationReader> WithId(Guid entityId, CancellationToken cancellationToken = new())
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var location = await context.Locations.AsQueryable().SingleOrDefaultAsync(l => l.Id == entityId, cancellationToken);

        return location;
    }

    public IAsyncEnumerable<LocationReader> GetHotels(CancellationToken cancellationToken = new())
    {
        var hotels = GetAll(cancellationToken)
            .Where(l => l.IsHotel)
            .OrderBy(l => l.Name);

        return hotels;
    }

    public IAsyncEnumerator<LocationReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
    {
        var locationAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

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