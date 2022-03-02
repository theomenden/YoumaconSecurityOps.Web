namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetStaffRolesQueryHandler : IStreamRequestHandler<GetStaffRolesQuery, StaffRole>
{
    private readonly IStaffRoleAccessor _staffRoles;
    
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffRolesQueryHandler(IStaffRoleAccessor staffRoles, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _staffRoles = staffRoles;
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<StaffRole> Handle(GetStaffRolesQuery request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken)
            .ConfigureAwait(false);
        
        await foreach (var role in _staffRoles.GetAll(context, cancellationToken).ConfigureAwait(false))
        {
            yield return role;
        }
    }
}