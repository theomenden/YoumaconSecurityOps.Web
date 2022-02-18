namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetIncidentsQueryHandler: IStreamRequestHandler<GetIncidentsQuery, IncidentReader>
{
    private readonly IIncidentAccessor _incidents;

    private readonly IMediator _mediator;

    private readonly ILogger<GetIncidentsQueryHandler> _logger;

    public GetIncidentsQueryHandler(IIncidentAccessor incidents, IMediator mediator, ILogger<GetIncidentsQueryHandler> logger)
    {
        _incidents = incidents;
        _mediator = mediator;
        _logger = logger;   
    }

    public IAsyncEnumerable<IncidentReader> Handle(GetIncidentsQuery request, CancellationToken cancellationToken)
    {
        var incidents = _incidents.GetAll(cancellationToken);

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