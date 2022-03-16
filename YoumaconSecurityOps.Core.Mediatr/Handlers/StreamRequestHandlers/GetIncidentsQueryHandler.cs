namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetIncidentsQueryHandler : IStreamRequestHandler<GetIncidentsQuery, IncidentReader>
{
    private readonly IIncidentAccessor _incidents;
    
    private readonly ILogger<GetIncidentsQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetIncidentsQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IIncidentAccessor incidents, ILogger<GetIncidentsQueryHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _incidents = incidents;
        _logger = logger;
    }

    public async IAsyncEnumerable<IncidentReader> Handle(GetIncidentsQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        await foreach (var incident in _incidents.GetAllAsync(context, cancellationToken).ConfigureAwait(false))
        {
            yield return incident;
        }
    }
}