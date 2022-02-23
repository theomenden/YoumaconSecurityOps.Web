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

    public IAsyncEnumerable<StaffReader> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new())
    {
        var staff =
            from member in dbContext.StaffMembers.AsAsyncEnumerable()
            join contact in dbContext.Contacts.AsAsyncEnumerable() on member.ContactId equals contact.Id
            join typeRoleMap in dbContext.StaffTypesRoles.AsAsyncEnumerable() on member.StaffTypeRoleId equals typeRoleMap.Id
            join staffType in dbContext.StaffTypes.AsAsyncEnumerable() on typeRoleMap.StaffTypeId equals staffType.Id
            join staffRole in dbContext.StaffRoles.AsAsyncEnumerable() on typeRoleMap.RoleId equals staffRole.Id
            orderby new { staffType, staffRole, contact.LastName }
            select new StaffReader
            {
                Id = member.Id,
                Contact = contact,
                BreakEndAt = member.BreakEndAt,
                BreakStartAt = member.BreakStartAt,
                IsBlackShirt = member.IsBlackShirt,
                IsOnBreak = member.IsOnBreak,
                IsRaveApproved = member.IsRaveApproved,
                NeedsCrashSpace = member.NeedsCrashSpace,
                ShirtSize = member.ShirtSize,
                StaffTypeRoleMaps = new List<StaffTypesRoles>(3) { typeRoleMap }

            };

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

    public IAsyncEnumerator<StaffReader> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        using var context = _dbContext.CreateDbContext();

        var staffAsyncEnumerator = GetAllAsync(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return staffAsyncEnumerator;
    }

    public async Task<bool> AddAsync(StaffReader entity, CancellationToken cancellationToken = default)
    {
        bool isAddSuccessful;

        try
        {
            await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

            await context.StaffMembers.AddAsync(entity, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

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
}