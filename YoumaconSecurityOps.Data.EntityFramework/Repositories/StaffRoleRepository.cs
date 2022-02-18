namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal class StaffRoleRepository : IStaffRoleAccessor
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    private readonly ILogger<StaffRoleRepository> _logger;

    public StaffRoleRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext, ILogger<StaffRoleRepository> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentException("Could Not be injected", nameof(dbContext));

        _logger = logger ?? throw new ArgumentException("Could Not be injected", nameof(logger));
    }


    public IAsyncEnumerable<StaffRole> GetAll(CancellationToken cancellationToken = default)
    {
        using var context = _dbContext.CreateDbContext();

        var staffRoles = context.StaffRoles.AsAsyncEnumerable()
            .OrderBy(sr => sr.Id);
            
        return staffRoles;
    }
    public async Task<StaffRole> WithId(int staffRoleId, CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var role = context.StaffRoles.SingleOrDefault(r => r.Id == staffRoleId);

        return role;
    }

    public IAsyncEnumerator<StaffRole> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        return GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);
    }
}