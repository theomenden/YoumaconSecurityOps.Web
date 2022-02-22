namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetIncidentsQueryHandler : IStreamRequestHandler<GetIncidentsQuery, IncidentReader>
{
    private readonly IIncidentAccessor _incidents;

    private readonly IMediator _mediator;

    private readonly ILogger<GetIncidentsQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetIncidentsQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IIncidentAccessor incidents, IMediator mediator, ILogger<GetIncidentsQueryHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _incidents = incidents;
        _mediator = mediator;
        _logger = logger;
    }

    public IAsyncEnumerable<IncidentReader> Handle(GetIncidentsQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var incidents = _incidents.GetAllAsync(context, cancellationToken);

        RaiseIncidentListQueriedEvent(request, cancellationToken);

        return incidents;
    }

    private Task RaiseIncidentListQueriedEvent(GetIncidentsQuery request, CancellationToken cancellationToken)
    {
        var e = new IncidentListQueriedEvent(null)
        {
            Aggregate = nameof(GetIncidentsQuery),
            MajorVersion = 1,
            Name = nameof(IncidentListQueriedEvent)
        };

        return _mediator.Publish(e, cancellationToken);
    }
}