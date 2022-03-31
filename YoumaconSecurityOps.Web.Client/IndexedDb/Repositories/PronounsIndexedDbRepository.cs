namespace YoumaconSecurityOps.Web.Client.IndexedDb.Repositories;

public class PronounsIndexedDbRepository: IIndexedDbRepository<Pronouns>
{
    private readonly YsecIndexedDbContext _indexedDbContext;

    public PronounsIndexedDbRepository(IModuleFactory jsModuleFactory)
    {
        _indexedDbContext = new YsecIndexedDbContext(jsModuleFactory);
    }

    public Task<List<Pronouns>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _indexedDbContext.StoredPronouns.ToList(cancellationToken);
    }

    public Task<Pronouns?> GetById(Int32 id)
    {
        return _indexedDbContext.StoredPronouns.Get(id);
    }

    public async Task<Pronouns> CreateOrUpdateAsync(Pronouns pronoun, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.StoredPronouns.Put(pronoun, cancellationToken);
        return await Task.FromResult(pronoun);
    }

    public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default)
    {
        return await _indexedDbContext.StoredPronouns.Count(cancellationToken) == 0;
    }

    public async Task CreateOrUpdateMultipleAsync(IEnumerable<Pronouns> entities, CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.StoredPronouns.BulkPut(entities, cancellationToken);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _indexedDbContext.StoredPronouns.Clear(cancellationToken);
    }
}

