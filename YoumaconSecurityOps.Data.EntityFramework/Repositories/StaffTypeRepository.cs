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

    public IAsyncEnumerable<StaffType> GetAll(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new CancellationToken())
    {
        var staffTypes = dbContext.StaffTypes.AsAsyncEnumerable()
            .OrderBy(t => t.Id);

        return staffTypes;
    }

    public async Task<StaffType> WithId(YoumaconSecurityDbContext dbContext, Int32 staffTypeId, CancellationToken cancellationToken = new CancellationToken())
    {
        var typeToFind = await dbContext.StaffTypes.SingleOrDefaultAsync(st => st.Id == staffTypeId, cancellationToken);

        return typeToFind;
    }

    public IAsyncEnumerator<StaffType> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        using var context = _dbContext.CreateDbContext();

        var staffTypeEnumerator = GetAll(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return staffTypeEnumerator;
    }
}