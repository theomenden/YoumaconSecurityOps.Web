namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal class GetPronounsQueryHandler : IRequestHandler<GetPronounsQuery, IEnumerable<Pronouns>>
{
    private readonly IStaffAccessor _staffInformation;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetPronounsQueryHandler(IStaffAccessor staffInformation, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _staffInformation = staffInformation;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<Pronouns>> Handle(GetPronounsQuery request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var pronouns = await _staffInformation.GetAllPronounsAsync(context, cancellationToken);

        return pronouns;
    }
}
