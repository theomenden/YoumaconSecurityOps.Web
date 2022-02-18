namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

/// <summary>
/// 
/// </summary>
/// <remarks>DON'T FORGET TO DEFINE THE GOD DAMN METHODS - Emma 8/27/2021 - 3:05am</remarks>
internal sealed class ShiftRepository:  IShiftAccessor, IShiftRepository
{
    private readonly ILogger<ShiftRepository> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    public ShiftRepository(ILogger<ShiftRepository> logger, IDbContextFactory<YoumaconSecurityDbContext> dbContext)
    {
        _logger = logger;

        _dbContext = dbContext;
    }

    public IAsyncEnumerable<ShiftReader> GetAll(CancellationToken cancellationToken = new ())
    {
        using var context = _dbContext.CreateDbContext();

        var shifts = context.Shifts.AsAsyncEnumerable();

        return shifts;
    }

    public async Task<ShiftReader> WithId(Guid entityId, CancellationToken cancellationToken = new ())
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var shift = await context.Shifts.AsQueryable()
            .SingleOrDefaultAsync(s => s.Id == entityId, cancellationToken);

        return shift;
    }

    public IAsyncEnumerator<ShiftReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
    {
        var getShiftAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

        return getShiftAsyncEnumerator;
    }

    public async Task<bool> AddAsync(ShiftReader entity, CancellationToken cancellationToken = default)
    {
        entity.CurrentLocationId = entity.StartingLocationId;

        bool addResult;

        try
        {
            await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

            context.Shifts.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            addResult = true;
        }
        catch (Exception e)
        {
            _logger.LogError("Task<bool> AddAsync(ShiftReader entity, CancellationToken cancellationToken = default) threw an exception: {e}", e.Message);

            addResult = false;
        }

        return addResult;
    }

    #region Shift Update Methods
    public async Task<ShiftReader> CheckIn(Guid shiftId, CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var shift = await context.Shifts.AsQueryable().SingleOrDefaultAsync(sh => sh.Id == shiftId, cancellationToken);

        var checkedInAt = DateTime.Now;

        shift.CheckedInAt = checkedInAt;

        shift.Notes += $"Checked In At: {checkedInAt:g}";

        await context.SaveChangesAsync(cancellationToken);

        return shift;
    }

    public async Task<ShiftReader> CheckOut(Guid shiftId, CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var shift = await context.Shifts.AsQueryable().SingleOrDefaultAsync(sh => sh.Id == shiftId, cancellationToken);

        var checkedOutAt = DateTime.Now;

        shift.CheckedOutAt = checkedOutAt;

        shift.Notes += $"Checked Out At {checkedOutAt:g}";

        await context.SaveChangesAsync(cancellationToken);

        return shift;
    }

    public async Task<ShiftReader> ReportIn(Guid shiftId, Guid currentLocationId, CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);


        var shiftToUpdate = await context.Shifts.AsQueryable().SingleOrDefaultAsync(sh => sh.Id == shiftId, cancellationToken);

        var reportedInAt = DateTime.Now;

        shiftToUpdate.LastReportedAt = reportedInAt;

        shiftToUpdate.CurrentLocationId = currentLocationId;

        shiftToUpdate.Notes += $"Reported In At: {reportedInAt:g}";

        await context.SaveChangesAsync(cancellationToken);

        return shiftToUpdate;
    }


    #endregion
}