namespace YsecOps.Core.Mediator.Handlers.QueryHandlers;

internal sealed class GetStaffTypesQueryHandler: IRequestHandler<GetStaffTypesQuery, List<StaffType>>
{
    private readonly IDbContextFactory<YoumaconSecurityOpsContext> _dbContextFactory;

    public GetStaffTypesQueryHandler(IDbContextFactory<YoumaconSecurityOpsContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<StaffType>> Handle(GetStaffTypesQuery request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var staffTypes = await context.StaffTypes
            .AsQueryable()
            .OrderBy(r => r.Id)
            .ToListAsync(cancellationToken);

        return staffTypes;
    }
}
