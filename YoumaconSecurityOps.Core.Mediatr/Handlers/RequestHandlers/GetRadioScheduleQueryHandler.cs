namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetRadioScheduleQueryHandler : IStreamRequestHandler<GetRadioSchedule, RadioScheduleReader>
{
    private readonly IRadioScheduleAccessor _radios;

    private readonly IMediator _mediator;

    private readonly ILogger<GetRadioScheduleQueryHandler> _logger;

    public GetRadioScheduleQueryHandler(IRadioScheduleAccessor radios, IMediator mediator, ILogger<GetRadioScheduleQueryHandler> logger)
    {
        _radios = radios;
        _mediator = mediator;
        _logger = logger;
    }

    public IAsyncEnumerable<RadioScheduleReader> Handle(GetRadioSchedule request, CancellationToken cancellationToken)
    {
        var radios = _radios.GetAll(cancellationToken);

        RaiseRadioScheduleQueriedEvent(request, cancellationToken);

        return radios;
    }

    private Task RaiseRadioScheduleQueriedEvent(GetRadioSchedule query, CancellationToken cancellationToken)
    {
        var e = new RadioScheduleQueriedEvent(null)
        {
            Aggregate = nameof(GetRadioSchedule),
            MajorVersion = 1,
            Name = nameof(RadioScheduleQueriedEvent)
        };

        return _mediator.Publish(e, cancellationToken);
    }
}