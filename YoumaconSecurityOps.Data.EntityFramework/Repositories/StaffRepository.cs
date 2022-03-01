using System.Linq.Expressions;
using YoumaconSecurityOps.Core.Shared.Extensions;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class StaffRepository : IStaffAccessor, IStaffRepository
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    private readonly ILogger<StaffRepository> _logger;

    public StaffRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext, ILogger<StaffRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    #region Get Methods
    public IAsyncEnumerable<StaffReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var staff = dbContext.StaffMembers
            .Include(st => st.Contact)
            .Include(st => st.StaffTypeRoleMaps)
                .ThenInclude(str => str.Role)
            .Include(st => st.StaffTypeRoleMaps)
                .ThenInclude(str => str.StaffTypeNavigation)
            .AsAsyncEnumerable();

        return staff;
    }

    public IAsyncEnumerable<StaffReader> GetAllThatMatchAsync(YoumaconSecurityDbContext dbContext, Expression<Func<StaffReader, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var staff = GetAllAsync(dbContext, cancellationToken).Where(predicate.Compile());

        return staff;
    }

    public async Task<StaffReader> WithIdAsync(YoumaconSecurityDbContext dbContext, Guid entityId, CancellationToken cancellationToken = new())
    {
        var staffMember = await GetAllAsync(dbContext, cancellationToken)
            .FirstOrDefaultAsync(s => s.Id == entityId, cancellationToken)
            .ConfigureAwait(false);

        return staffMember;
    }
    #endregion

    public IAsyncEnumerator<StaffReader> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        using var context = _dbContext.CreateDbContext();

        var staffAsyncEnumerator = GetAllAsync(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return staffAsyncEnumerator;
    }

    #region Mutation Methods
    public async Task<bool> AddAsync(YoumaconSecurityDbContext dbContext, StaffReader entity, CancellationToken cancellationToken = default)
    {
        bool isAddSuccessful;

        try
        {
            dbContext.StaffMembers.Add(entity);

            await dbContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            isAddSuccessful = true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An exception occurred while attempting to create a staff record for: {@staffMember}, {ex}", entity, ex.InnerException?.Message ?? ex.Message);

            isAddSuccessful = false;
        }

        return isAddSuccessful;
    }

    public async Task<StaffReader> SendOnBreak(Guid staffId, CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var staffMemberToSendOnBreak = await
            context.StaffMembers.AsQueryable().SingleOrDefaultAsync(st => st.Id == staffId, cancellationToken);

        try
        {

            if (staffMemberToSendOnBreak is not null)
            {
                staffMemberToSendOnBreak.BreakStartAt = DateTime.Now;
                staffMemberToSendOnBreak.IsOnBreak = true;

                await context.SaveChangesAsync(cancellationToken);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError("An exception occurred trying to send staff member {staffId} on break: {ex}",
                ex.InnerException?.Message ?? ex.Message);
        }

        return staffMemberToSendOnBreak;
    }

    public async Task<StaffReader> ReturnFromBreak(Guid staffId, CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var staffMemberToReturnFromBreak = await
            context.StaffMembers.AsQueryable()
                .SingleOrDefaultAsync(st => st.Id == staffId, cancellationToken);

        try
        {

            if (staffMemberToReturnFromBreak is not null)
            {
                staffMemberToReturnFromBreak.BreakStartAt = DateTime.Now;
                staffMemberToReturnFromBreak.IsOnBreak = true;

                await context.SaveChangesAsync(cancellationToken);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError("An exception occured trying to return staff member {staffId} from their break: {ex}",
                ex.InnerException?.Message ?? ex.Message);
        }

        return staffMemberToReturnFromBreak;
    }
    #endregion
}