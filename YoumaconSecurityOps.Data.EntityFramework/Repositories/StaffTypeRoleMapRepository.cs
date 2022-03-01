namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class StaffTypeRoleMapRepository: IStaffRoleMapRepository
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly ILogger<StaffTypeRoleMapRepository> _logger;

    public StaffTypeRoleMapRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, ILogger<StaffTypeRoleMapRepository> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public IAsyncEnumerator<StaffTypesRoles> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        using var context = _dbContextFactory.CreateDbContext();

        var staffRoleMapEnumerator = context.StaffTypesRoles.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

        return staffRoleMapEnumerator;
    }

    public async Task<bool> AddAsync(YoumaconSecurityDbContext dbContext, StaffTypesRoles entity,
        CancellationToken cancellationToken = default)
    {
        var successfulAddResult = false;
        
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            context.StaffTypesRoles.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            successfulAddResult = true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Couldn't store RoleMap for user with Id {userId}: {@ex}", entity.StaffId, ex);
        }

        return successfulAddResult;
    }
}

