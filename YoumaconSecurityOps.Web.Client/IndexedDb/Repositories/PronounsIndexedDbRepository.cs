namespace YoumaconSecurityOps.Web.Client.IndexedDb.Repositories;

public class PronounsIndexedDbRepository: IIndexedDbRepository<Pronoun>
{
    private readonly YsecIndexedDbContext _indexedDbContext;

    public PronounsIndexedDbRepository(IModuleFactory jsModuleFactory)
    {
        _indexedDbContext = new YsecIndexedDbContext(jsModuleFactory);
    }

    public Task<List<Pronoun>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _indexedDbContext.StoredPronouns.ToList(cancellationToken);
    }

    public Task<Pronoun?> GetById(Int32 id)
    {
        return _indexedDbContext.StoredPronouns.Get(id);
    }

    public async Task<Pronoun> CreateOrUpdateAsync(Pronoun pronoun, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.StoredPronouns.Put(pronoun, cancellationToken);
        return await Task.FromResult(pronoun);
    }

    public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default)
    {
        return await _indexedDbContext.StoredPronouns.Count(cancellationToken) == 0;
    }

    public async Task CreateOrUpdateMultipleAsync(IEnumerable<Pronoun> entities, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.StoredPronouns.BulkPut(entities, cancellationToken);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.StoredPronouns.Clear(cancellationToken);
    }
}

