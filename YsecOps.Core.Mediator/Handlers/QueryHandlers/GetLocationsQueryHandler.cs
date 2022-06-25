namespace YsecOps.Core.Mediator.Handlers.QueryHandlers;

internal sealed class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, List<Location>>
{
    private readonly IDbContextFactory<YoumaconSecurityOpsContext> _dbContextFactory;

    public GetLocationsQueryHandler(IDbContextFactory<YoumaconSecurityOpsContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Location>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var pronouns = context.Locations.OrderBy(l => l.Name);

        return await pronouns.ToListAsync(cancellationToken);
    }
}
