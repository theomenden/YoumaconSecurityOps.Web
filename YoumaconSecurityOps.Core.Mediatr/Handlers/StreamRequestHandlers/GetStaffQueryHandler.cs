namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetStaffQueryHandler: IStreamRequestHandler<GetStaffQuery, StaffReader>
{
    private readonly IStaffAccessor _staff;
    
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffQueryHandler(IStaffAccessor staff, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _staff = staff;
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<StaffReader> Handle(GetStaffQuery request, [EnumeratorCancellation]CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        await foreach (var member in _staff.GetAllAsync(context, cancellationToken).ConfigureAwait(false))
        {
            yield return member;
        }
    }
    
}