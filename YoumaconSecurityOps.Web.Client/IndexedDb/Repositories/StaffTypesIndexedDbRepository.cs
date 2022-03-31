namespace YoumaconSecurityOps.Web.Client.IndexedDb.Repositories;
public class StaffTypesIndexedDbRepository: IIndexedDbRepository<StaffType>
{
    private readonly YsecIndexedDbContext _indexedDbContext;

    public StaffTypesIndexedDbRepository(IModuleFactory jsModuleFactory)
    {
        _indexedDbContext = new(jsModuleFactory);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.StaffTypes.Clear(cancellationToken);
    }

    public async Task<List<StaffType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _indexedDbContext.StaffTypes.ToList(cancellationToken);
    }

    public async Task<StaffType> CreateOrUpdateAsync(StaffType entity, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.StaffTypes.Put(entity, cancellationToken);
        return await Task.FromResult(entity);
    }

    public async Task CreateOrUpdateMultipleAsync(IEnumerable<StaffType> entities, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.StaffTypes.BulkPut(entities, cancellationToken);
    }

    public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default)
    {
        return await _indexedDbContext.StaffTypes.Count(cancellationToken) == 0;
    }
}

