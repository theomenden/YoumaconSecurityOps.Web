using System.Linq.Expressions;
using YoumaconSecurityOps.Core.Shared.Extensions;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

/// <summary>
/// 
/// </summary>
/// <remarks>DON'T FORGET TO DEFINE THE GOD DAMN METHODS - Emma 8/27/2021 - 3:05am</remarks>
internal sealed class ShiftRepository : IShiftAccessor, IShiftRepository
{
    private readonly ILogger<ShiftRepository> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    public ShiftRepository(ILogger<ShiftRepository> logger, IDbContextFactory<YoumaconSecurityDbContext> dbContext)
    {
        _logger = logger;

        _dbContext = dbContext;
    }

    #region Get Methods
    public IAsyncEnumerable<ShiftReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var shifts = dbContext.Shifts
            .Include(sh => sh.StaffMember)
                .ThenInclude(st => st.Contact)
            .Include(sh => sh.StaffMember)
                .ThenInclude(st => st.StaffTypeRoleMaps)
                    .ThenInclude(sr => sr.Role)
            .Include(sh => sh.StaffMember)
                .ThenInclude(st => st.StaffTypeRoleMaps)
                    .ThenInclude(str => str.StaffTypeNavigation)
            .Include(sh => sh.CurrentLocation)
            .Include(sh => sh.StartingLocationNavigation)
            .AsAsyncEnumerable();

        return shifts;
    }

    public IAsyncEnumerable<ShiftReader> GetAllThatMatchAsync(YoumaconSecurityDbContext dbContext, Expression<Func<ShiftReader, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var shifts = dbContext.Shifts
            .Include(sh => sh.StaffMember)
                .ThenInclude(st => st.Contact)
            .Include(sh => sh.StaffMember)
                .ThenInclude(st => st.StaffTypeRoleMaps)
                    .ThenInclude(sr => sr.Role)
            .Include(sh => sh.StaffMember)
                .ThenInclude(st => st.StaffTypeRoleMaps)
                    .ThenInclude(str => str.StaffTypeNavigation)
            .Include(sh => sh.CurrentLocation)
            .Include(sh => sh.StartingLocationNavigation)
            .Where(predicate)
            .AsAsyncEnumerable();

        return shifts;
    }

    public async Task<ShiftReader> WithIdAsync(YoumaconSecurityDbContext dbContext, Guid entityId, CancellationToken cancellationToken = new())
    {
        var shift = await dbContext.Shifts
            .Include(sh => sh.StaffMember)
                .ThenInclude(st => st.Contact)
            .Include(sh => sh.StaffMember)
                .ThenInclude(st => st.StaffTypeRoleMaps)
                    .ThenInclude(sr => sr.Role)
            .Include(sh => sh.StaffMember)
                .ThenInclude(st => st.StaffTypeRoleMaps)
                    .ThenInclude(str => str.StaffTypeNavigation)
            .Include(sh => sh.CurrentLocation)
            .Include(sh => sh.StartingLocationNavigation)
            .AsQueryable()
            .SingleOrDefaultAsync(s => s.Id == entityId, cancellationToken);

        return shift;
    }
    #endregion

    public IAsyncEnumerator<ShiftReader> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        using var context = _dbContext.CreateDbContext();

        var getShiftAsyncEnumerator = GetAllAsync(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return getShiftAsyncEnumerator;
    }

    #region Mutation Methods
    public async Task<bool> AddAsync(YoumaconSecurityDbContext dbContext, ShiftReader entity, CancellationToken cancellationToken = default)
    {
        entity.CurrentLocationId = entity.StartingLocationId;

        bool addResult;

        try
        {
            dbContext.Shifts.Add(entity);

            await dbContext.SaveChangesAsync(cancellationToken);

            addResult = true;
        }
        catch (Exception e)
        {
            _logger.LogError("Task<bool> AddAsync(ShiftReader entity, CancellationToken cancellationToken = default) threw an exception: {e}", e.Message);

            addResult = false;
        }

        return addResult;
    }
    
    public async Task<ShiftReader> CheckIn(YoumaconSecurityDbContext dbContext, Guid shiftId, CancellationToken cancellationToken = default)
    {
        var shift = await dbContext.Shifts.AsQueryable().SingleOrDefaultAsync(sh => sh.Id == shiftId, cancellationToken);

        var checkedInAt = DateTime.Now;

        shift.CheckedInAt = checkedInAt;

        shift.Notes += $"Checked In At: {checkedInAt:g}";

        await dbContext.SaveChangesAsync(cancellationToken);

        return shift;
    }

    public async Task<ShiftReader> CheckOut(YoumaconSecurityDbContext dbContext, Guid shiftId, CancellationToken cancellationToken = default)
    {
        var shift = await dbContext.Shifts.AsQueryable().SingleOrDefaultAsync(sh => sh.Id == shiftId, cancellationToken);

        var checkedOutAt = DateTime.Now;

        shift.CheckedOutAt = checkedOutAt;

        shift.Notes += $"Checked Out At {checkedOutAt:g}";

        await dbContext.SaveChangesAsync(cancellationToken);

        return shift;
    }

    public async Task<ShiftReader> ReportIn(YoumaconSecurityDbContext dbContext, Guid shiftId, Guid currentLocationId, CancellationToken cancellationToken = default)
    {
        var shiftToUpdate = await dbContext.Shifts.AsQueryable().SingleOrDefaultAsync(sh => sh.Id == shiftId, cancellationToken);

        var reportedInAt = DateTime.Now;

        shiftToUpdate.LastReportedAt = reportedInAt;

        shiftToUpdate.CurrentLocationId = currentLocationId;

        shiftToUpdate.Notes += $"Reported In At: {reportedInAt:g}";

        await dbContext.SaveChangesAsync(cancellationToken);

        return shiftToUpdate;
    }
    #endregion
}