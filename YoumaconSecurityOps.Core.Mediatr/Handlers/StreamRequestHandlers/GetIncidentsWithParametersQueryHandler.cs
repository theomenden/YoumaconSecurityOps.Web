namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetIncidentsWithParametersQueryHandler : IStreamRequestHandler<GetIncidentsWithParametersQuery, IncidentReader>
{
    private readonly IIncidentAccessor _staff;

    private readonly IMediator _mediator;

    private readonly ILogger<GetIncidentsWithParametersQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetIncidentsWithParametersQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IIncidentAccessor staff, IMediator mediator, ILogger<GetIncidentsWithParametersQueryHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _staff = staff;
        _mediator = mediator;
        _logger = logger;
    }


    public IAsyncEnumerable<IncidentReader> Handle(GetIncidentsWithParametersQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();
        
        var filteredIncidents = Filter(request.Parameters, _staff.GetAllAsync(context,cancellationToken));

        return filteredIncidents;
    }

    private static IAsyncEnumerable<IncidentReader> Filter(IncidentQueryStringParameters parameters, IAsyncEnumerable<IncidentReader> incidents)
    {
        return incidents
            .Where(i => i.Title.Equals(parameters.Title))
            .Where(i => parameters.StaffIds.Contains(i.ReportedById))
            .Where(i => parameters.StaffIds.Contains(i.RecordedById))
            .Where(i => i.Severity == parameters.Severity);
    }
}