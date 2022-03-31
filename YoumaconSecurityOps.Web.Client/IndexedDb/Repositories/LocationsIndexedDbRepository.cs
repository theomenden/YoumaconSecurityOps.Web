namespace YoumaconSecurityOps.Web.Client.IndexedDb.Repositories;

public class LocationsIndexedDbRepository : IIndexedDbRepository<LocationReader>
{
    private readonly YsecIndexedDbContext _indexedDbContext;

    public LocationsIndexedDbRepository(IModuleFactory jsModuleFactory)
    {
        _indexedDbContext = new YsecIndexedDbContext(jsModuleFactory);
    }

    public Task<List<LocationReader>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _indexedDbContext.Locations.ToList(cancellationToken);
    }

    public Task<LocationReader?> GetById(Guid id)
    {
        return _indexedDbContext.Locations.Get(id);
    }

    public async Task<LocationReader> CreateOrUpdateAsync(LocationReader location, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.Locations.Put(location, cancellationToken);
        return await Task.FromResult(location);
    }

    public async Task CreateOrUpdateMultipleAsync(IEnumerable<LocationReader> entities, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.Locations.BulkPut(entities, cancellationToken);
    }

    public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default)
    {
        return await _indexedDbContext.Locations.Count(cancellationToken) == 0;
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.Locations.Clear(cancellationToken);
    }
}

