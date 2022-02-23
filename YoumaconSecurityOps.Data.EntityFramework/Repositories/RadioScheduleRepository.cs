using System.Linq.Expressions;
using YoumaconSecurityOps.Core.Shared.Extensions;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class RadioScheduleRepository: IRadioScheduleAccessor, IRadioScheduleRepository
{
    private readonly ILogger<RadioScheduleRepository> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    public RadioScheduleRepository(ILogger<RadioScheduleRepository> logger, IDbContextFactory<YoumaconSecurityDbContext> dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IAsyncEnumerable<RadioScheduleReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new ())
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

    public async Task<RadioScheduleReader> WithIdAsync(YoumaconSecurityDbContext dbContext, Guid entityId, CancellationToken cancellationToken = new ())
    {
        var radio = await dbContext.RadioSchedules
            .AsQueryable()
            .SingleOrDefaultAsync(r => r.Id == entityId, cancellationToken);

        return radio;
    }

    public IAsyncEnumerator<RadioScheduleReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
    {
        using var context = _dbContext.CreateDbContext();

        var radioScheduleAsyncEnumerator = GetAllAsync(context,cancellationToken).GetAsyncEnumerator(cancellationToken);

        return radioScheduleAsyncEnumerator;
    }

    public async Task<bool> AddAsync(RadioScheduleReader entity, CancellationToken cancellationToken = default)
    {
        var addResult = false;

        try
        {
            await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

            context.RadioSchedules.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            addResult = true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Failed to add Radio: {@entity}, Reason {ex}", entity, ex.Message);
        }

        return addResult;

    }
}