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

    public IAsyncEnumerable<StaffType> GetAll(CancellationToken cancellationToken = new CancellationToken())
    {
        using var context = _dbContext.CreateDbContext();

        var staffTypes = context.StaffTypes.AsAsyncEnumerable()
            .OrderBy(t => t.Id);

        return staffTypes;
    }

    public async Task<StaffType> WithId(Int32 staffTypeId, CancellationToken cancellationToken = new CancellationToken())
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var typeToFind = await context.StaffTypes.SingleOrDefaultAsync(st => st.Id == staffTypeId, cancellationToken);

        return typeToFind;
    }

    public IAsyncEnumerator<StaffType> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        var staffTypeEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

        return staffTypeEnumerator;
    }
}