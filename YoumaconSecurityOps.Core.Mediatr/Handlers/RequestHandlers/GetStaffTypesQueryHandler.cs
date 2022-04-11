namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetStaffTypesQueryHandler : IRequestHandler<GetStaffTypesQuery, IEnumerable<StaffType>>
{
    private readonly IStaffTypeAccessor _staffTypes;
    
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffTypesQueryHandler(IStaffTypeAccessor staffTypes, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _staffTypes = staffTypes;
        _dbContextFactory = dbContextFactory;
    }

    public Task<IEnumerable<StaffType>> Handle(GetStaffTypesQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        return _staffTypes.GetAllAsync(context, cancellationToken);
    }
}