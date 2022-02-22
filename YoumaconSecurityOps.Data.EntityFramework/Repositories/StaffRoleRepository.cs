
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


    public IAsyncEnumerable<StaffRole> GetAll(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var staffRoles = dbContext.StaffRoles.AsAsyncEnumerable()
            .OrderBy(sr => sr.Id);
            
        return staffRoles;
    }
    public async Task<StaffRole> WithId(YoumaconSecurityDbContext dbContext, int staffRoleId, CancellationToken cancellationToken = default)
    {
        var role = dbContext.StaffRoles.SingleOrDefault(r => r.Id == staffRoleId);

        return role;
    }

    public IAsyncEnumerator<StaffRole> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        using var context = _dbContext.CreateDbContext();

        return GetAll(context, cancellationToken).GetAsyncEnumerator(cancellationToken);
    }
}