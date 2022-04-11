
using System.Collections;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal class StaffRoleRepository : IStaffRoleAccessor
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    public StaffRoleRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext, ILogger<StaffRoleRepository> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentException("Could Not be injected", nameof(dbContext));
    }


    public async Task<IEnumerable<StaffRole>> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var staffRoles = dbContext.StaffRoles.OrderBy(sr => sr.Id);
            
        return await staffRoles.ToListAsync(cancellationToken);
    }
    public Task<StaffRole> WithId(YoumaconSecurityDbContext dbContext, int staffRoleId, CancellationToken cancellationToken = default)
    {
        var role = dbContext.StaffRoles.SingleOrDefault(r => r.Id == staffRoleId);

        return Task.FromResult(role);
    }

    public IEnumerator<StaffRole> GetEnumerator()
    {
        var context = _dbContext.CreateDbContext();

        var staffRoleAsyncEnumerator = GetAllAsync(context).Result.GetEnumerator();

        return staffRoleAsyncEnumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}