namespace YoumaconSecurityOps.Web.Client.IndexedDb.Repositories;

public class StaffRolesIndexedDbRepository: IIndexedDbRepository<StaffRole>
{
    private readonly YsecIndexedDbContext _indexedDbContext;

    public StaffRolesIndexedDbRepository(IModuleFactory jsModuleFactory)
    {
        _indexedDbContext = new YsecIndexedDbContext(jsModuleFactory);
    }

    public Task<List<StaffRole>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _indexedDbContext.Roles.ToList(cancellationToken);
    }

    public Task<StaffRole> GetByIdAsync(Int32 id, CancellationToken cancellationToken = default)
    {
        return _indexedDbContext.Roles.Get(id, cancellationToken);
    }

    public async Task<StaffRole> CreateOrUpdateAsync(StaffRole entity, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.Roles.Put(entity, cancellationToken);
        return await Task.FromResult(entity);
    }

    public async Task CreateOrUpdateMultipleAsync(IEnumerable<StaffRole> entities, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.Roles.BulkPut(entities, cancellationToken);
    }

    public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default)
    {
        return await _indexedDbContext.Roles.Count(cancellationToken) == 0;
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.Roles.Clear(cancellationToken);
    }
}
