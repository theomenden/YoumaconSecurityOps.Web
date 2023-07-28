namespace YsecOps.Core.Mediator.Handlers.QueryHandlers;
internal sealed class GetStaffRolesQueryHandler: IRequestHandler<GetStaffRolesQuery, List<Role>>
{
    private readonly IDbContextFactory<YoumaconSecurityOpsContext> _dbContextFactory;

    public GetStaffRolesQueryHandler(IDbContextFactory<YoumaconSecurityOpsContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Role>> Handle(GetStaffRolesQuery request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var roles = await context.Roles
            .AsQueryable()
            .OrderBy(r => r.Id)
            .ToListAsync(cancellationToken);

        return roles;
    }
}
