namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetIncidentsWithParametersQueryHandler : IStreamRequestHandler<GetIncidentsWithParametersQuery, IncidentReader>
{
    private readonly IIncidentAccessor _staff;

    private readonly IMediator _mediator;

    private readonly ILogger<GetIncidentsWithParametersQueryHandler> _logger;

    public GetIncidentsWithParametersQueryHandler(IIncidentAccessor staff, IMediator mediator, ILogger<GetIncidentsWithParametersQueryHandler> logger)
    {
        _staff = staff;
        _mediator = mediator;
        _logger = logger;
    }


    public IAsyncEnumerable<IncidentReader> Handle(GetIncidentsWithParametersQuery request, CancellationToken cancellationToken)
    {
        RaiseStaffListQueriedEvent(request.Parameters, cancellationToken);

        var filteredIncidents = Filter(request.Parameters, _staff.GetAll(cancellationToken));

        return filteredIncidents;
    }

    private static IAsyncEnumerable<IncidentReader> Filter(IncidentQueryStringParameters parameters, IAsyncEnumerable<IncidentReader> incidents)
    {
        return incidents
            .Where(i => i.Title.Equals(parameters.Title))
            .Where(i => parameters.StaffIds.Contains(i.ReportedById))
            .Where(i => parameters.StaffIds.Contains(i.RecordedById))
            .Where(i => i.Severity == (int)parameters.Severity);
                
    }

    private Task RaiseStaffListQueriedEvent(IncidentQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        var e = new IncidentListQueriedEvent(parameters)
        {
            Aggregate = nameof(IncidentQueryStringParameters),
            MajorVersion = 1,
            MinorVersion = 1,
            Name = nameof(IncidentListQueriedEvent)
        };

        return _mediator.Publish(e, cancellationToken);
    }
}