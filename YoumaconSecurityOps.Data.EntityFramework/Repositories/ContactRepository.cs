using System.Linq.Expressions;
using YoumaconSecurityOps.Core.Shared.Extensions;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class ContactRepository : IContactAccessor, IContactRepository
{
    private readonly ILogger<ContactRepository> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    public ContactRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext, ILogger<ContactRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    #region Get Methods
    public IAsyncEnumerable<ContactReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var contacts = dbContext.Contacts
            .AsAsyncEnumerable()
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.PreferredName)
            .ThenBy(c => c.CreatedOn);

        return contacts;
    }

    public IAsyncEnumerable<ContactReader> GetAllThatMatchAsync(YoumaconSecurityDbContext dbContext, Expression<Func<ContactReader, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var contactsThatMatch = dbContext.Contacts.FindAllAsync(predicate);

        return contactsThatMatch;
    }

    public async Task<ContactReader> WithIdAsync(YoumaconSecurityDbContext dbContext, Guid entityId, CancellationToken cancellationToken = new())
    {
        var contact = await GetAllAsync(dbContext, cancellationToken)
            .SingleOrDefaultAsync(c => c.Id == entityId, cancellationToken);

        return contact;
    }
    #endregion

    public IAsyncEnumerator<ContactReader> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        using var context = _dbContext.CreateDbContext();

        var contactAsyncEnumerator = GetAllAsync(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return contactAsyncEnumerator;
    }

    public async Task<bool> AddAsync(YoumaconSecurityDbContext dbContext, ContactReader entity, CancellationToken cancellationToken = default)
    {
        try
        {
            dbContext.Contacts.Add(entity);

            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unable to store entity {@entity}. Exception : {@ex}", entity, ex);
            return false;
        }
    }
}