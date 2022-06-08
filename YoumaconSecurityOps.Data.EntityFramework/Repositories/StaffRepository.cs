using System.Linq.Expressions;
using YoumaconSecurityOps.Core.Shared.Extensions;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class StaffRepository : IStaffAccessor, IStaffRepository
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    private readonly ILogger<StaffRepository> _logger;

    internal StaffRepository()
    {
    }

    public StaffRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext, ILogger<StaffRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    #region Get Methods
    public IAsyncEnumerable<StaffReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var staff = dbContext.StaffMembers
            .Include(st => st.ContactInformation)
                .ThenInclude(c => c.Pronoun)
            .Include(st => st.StaffTypesRoles)
                .ThenInclude(str => str.StaffRole)
            .Include(st => st.StaffTypesRoles)
                .ThenInclude(str => str.StaffType)
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

    public async Task<IEnumerable<Pronoun>> GetAllPronounsAsync(YoumaconSecurityDbContext context, CancellationToken cancellationToken = default)
    {
        var pronouns = await context.Pronouns.ToArrayAsync(cancellationToken);

        return pronouns;
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

    public async Task<StaffReader> SendOnBreak(YoumaconSecurityDbContext dbContext, Guid staffId, CancellationToken cancellationToken = default)
    {
        var staffMemberToSendOnBreak = await
            dbContext.StaffMembers.AsQueryable().SingleOrDefaultAsync(st => st.Id == staffId, cancellationToken);

        try
        {

            if (staffMemberToSendOnBreak is not null)
            {
                staffMemberToSendOnBreak.BreakStartAt = DateTime.Now;
                staffMemberToSendOnBreak.IsOnBreak = true;

                await dbContext.SaveChangesAsync(cancellationToken);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError("An exception occurred trying to send staff member {staffId} on break: {ex}",
                staffId,ex.InnerException?.Message ?? ex.Message);
        }

        return staffMemberToSendOnBreak;
    }

    public async Task<StaffReader> ReturnFromBreak(YoumaconSecurityDbContext dbContext, Guid staffId, CancellationToken cancellationToken = default)
    {
        var staffMemberToReturnFromBreak = await
            dbContext.StaffMembers.AsQueryable()
                .SingleOrDefaultAsync(st => st.Id == staffId, cancellationToken);

        try
        {

            if (staffMemberToReturnFromBreak is not null)
            {
                staffMemberToReturnFromBreak.BreakEndAt = DateTime.Now;
                staffMemberToReturnFromBreak.IsOnBreak = false;

                await dbContext.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError("An exception occurred trying to return staff member {staffId} from their break: {ex}",
                staffId,ex.InnerException?.Message ?? ex.Message);
        }

        return staffMemberToReturnFromBreak;
    }
    #endregion
}