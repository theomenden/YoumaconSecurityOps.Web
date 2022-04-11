using System.Collections;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class StaffTypeRepository: IStaffTypeAccessor
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    private readonly ILogger<StaffTypeRepository> _logger;

    public StaffTypeRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext, ILogger<StaffTypeRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;   
    }


    public async Task<IEnumerable<StaffType>> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var staffTypes = dbContext.StaffTypes
            .OrderByDescending(st => st.Id);

        return await staffTypes.ToListAsync(cancellationToken);
    }

    public async Task<StaffType> WithId(YoumaconSecurityDbContext dbContext, Int32 staffTypeId, CancellationToken cancellationToken = new CancellationToken())
    {
        var typeToFind = await dbContext.StaffTypes.SingleOrDefaultAsync(st => st.Id == staffTypeId, cancellationToken);

        return typeToFind;
    }
    
    public IEnumerator<StaffType> GetEnumerator()
    {
        using var context = _dbContext.CreateDbContext();

        var staffTypeEnumerator = GetAllAsync(context).Result.GetEnumerator();

        return staffTypeEnumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}