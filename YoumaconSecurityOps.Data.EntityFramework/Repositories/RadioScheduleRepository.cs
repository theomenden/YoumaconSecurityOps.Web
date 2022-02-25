using System.Linq.Expressions;
using YoumaconSecurityOps.Core.Shared.Extensions;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class RadioScheduleRepository : IRadioScheduleAccessor, IRadioScheduleRepository
{
    private readonly ILogger<RadioScheduleRepository> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    public RadioScheduleRepository(ILogger<RadioScheduleRepository> logger, IDbContextFactory<YoumaconSecurityDbContext> dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    #region  Get Methods
    public IAsyncEnumerable<RadioScheduleReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var radios = dbContext.RadioSchedules
            .AsAsyncEnumerable()
            .OrderBy(r => r.RadioNumber);

        return radios;
    }

    public IAsyncEnumerable<RadioScheduleReader> GetAllThatMatchAsync(YoumaconSecurityDbContext dbContext, Expression<Func<RadioScheduleReader, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var radios = dbContext.RadioSchedules.FindAllAsync(predicate);

        return radios;
    }

    public async Task<RadioScheduleReader> WithIdAsync(YoumaconSecurityDbContext dbContext, Guid entityId, CancellationToken cancellationToken = new())
    {
        var radio = await dbContext.RadioSchedules
            .AsQueryable()
            .SingleOrDefaultAsync(r => r.Id == entityId, cancellationToken);

        return radio;
    }
    #endregion

    public IAsyncEnumerator<RadioScheduleReader> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        using var context = _dbContext.CreateDbContext();

        var radioScheduleAsyncEnumerator = GetAllAsync(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return radioScheduleAsyncEnumerator;
    }

    public async Task<bool> AddAsync(YoumaconSecurityDbContext dbContext,RadioScheduleReader entity, CancellationToken cancellationToken = default)
    {
        var addResult = false;

        try
        {

            dbContext.RadioSchedules.Add(entity);

            await dbContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            addResult = true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Failed to add Radio: {@entity}, Reason {ex}", entity, ex.Message);
        }

        return addResult;

    }
}