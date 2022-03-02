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

    public IAsyncEnumerable<IncidentReader> Handle(GetIncidentsQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var incidents = _incidents.GetAllAsync(context, cancellationToken);
        
        return incidents;
    }
}