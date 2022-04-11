namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetStaffRolesQueryHandler : IRequestHandler<GetStaffRolesQuery, IEnumerable<StaffRole>>
{
    private readonly IStaffRoleAccessor _staffRoles;
    
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffRolesQueryHandler(IStaffRoleAccessor staffRoles, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _staffRoles = staffRoles;
        _dbContextFactory = dbContextFactory;
    }

    public Task<IEnumerable<StaffRole>> Handle(GetStaffRolesQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        return _staffRoles.GetAllAsync(context, cancellationToken);
    }
}