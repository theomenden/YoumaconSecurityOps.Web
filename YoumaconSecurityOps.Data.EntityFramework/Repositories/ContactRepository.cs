namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class ContactRepository: IContactAccessor, IContactRepository
{
    private readonly ILogger<ContactRepository> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;
        
    public ContactRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext, ILogger<ContactRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public IAsyncEnumerable<ContactReader> GetAll(CancellationToken cancellationToken = new ())
    {
        using var context = _dbContext.CreateDbContext();

        var contacts = context.Contacts
            .AsAsyncEnumerable()
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.PreferredName)
            .ThenBy(c => c.CreatedOn);

        return contacts;
    }

    public async Task<ContactReader> WithId(Guid entityId, CancellationToken cancellationToken = new ())
    {
        var contact = await GetAll(cancellationToken)
            .SingleOrDefaultAsync(c => c.Id == entityId, cancellationToken);

        return contact;
    }

    public IAsyncEnumerator<ContactReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
    {
        var contactAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

        return contactAsyncEnumerator;
    }

    public async Task<bool> AddAsync(ContactReader entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

            context.Contacts.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unable to store entity {@entity}. Exception : {@ex}", entity, ex);
            return false;
        }
    }
}