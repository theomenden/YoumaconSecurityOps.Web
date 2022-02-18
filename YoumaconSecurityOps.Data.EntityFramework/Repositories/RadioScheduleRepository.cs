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

    public IAsyncEnumerable<RadioScheduleReader> GetAll(CancellationToken cancellationToken = new ())
    {
        using var context = _dbContext.CreateDbContext();

        var radios = context.RadioSchedules
            .AsAsyncEnumerable()
            .OrderBy(r => r.RadioNumber);

        return radios;
    }

    public async Task<RadioScheduleReader> WithId(Guid entityId, CancellationToken cancellationToken = new ())
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var radio = await context.RadioSchedules.AsQueryable().SingleOrDefaultAsync(r => r.Id == entityId, cancellationToken);

        return radio;
    }

    public IAsyncEnumerator<RadioScheduleReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
    {
        var radioScheduleAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

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