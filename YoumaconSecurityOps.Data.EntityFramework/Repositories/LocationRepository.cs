using System.Linq.Expressions;
using YoumaconSecurityOps.Core.Shared.Extensions;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class LocationRepository : ILocationAccessor, ILocationRepository
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    public LocationRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentException("Could not register DbContext: ", nameof(dbContext));
    }

    #region Get Methods
    public IAsyncEnumerable<LocationReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var locations = dbContext.Locations
            .OrderBy(l => l.Name)
            .AsAsyncEnumerable();

        return locations;
    }

    public IAsyncEnumerable<LocationReader> GetAllThatMatchAsync(YoumaconSecurityDbContext dbContext, Expression<Func<LocationReader, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var locations = dbContext.Locations.FindAllAsync(predicate);

        return locations;
    }

    public async Task<LocationReader> WithIdAsync(YoumaconSecurityDbContext dbContext, Guid entityId, CancellationToken cancellationToken = new())
    {
        var location = await dbContext.Locations.AsQueryable()
            .SingleOrDefaultAsync(l => l.Id == entityId, cancellationToken);

        return location;
    }

    public IAsyncEnumerable<LocationReader> GetHotels(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var hotels = GetAllAsync(dbContext, cancellationToken)
            .Where(l => l.IsHotel)
            .OrderBy(l => l.Name);

        return hotels;
    }

    public async Task<IEnumerable<LocationReader>> GetAllLocationsAsync(YoumaconSecurityDbContext dbContext,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var locations = dbContext.Locations
            .OrderBy(l => l.Name)
            .AsQueryable();

        return await locations.ToListAsync(cancellationToken);
    }

    #endregion

    public IAsyncEnumerator<LocationReader> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        using var context = _dbContext.CreateDbContext();

        var locationAsyncEnumerator = GetAllAsync(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return locationAsyncEnumerator;
    }

    public async Task<bool> AddAsync(YoumaconSecurityDbContext dbContext, LocationReader entity, CancellationToken cancellationToken = new())
    {
        try
        {
            dbContext.Locations.Add(entity);

            await dbContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}