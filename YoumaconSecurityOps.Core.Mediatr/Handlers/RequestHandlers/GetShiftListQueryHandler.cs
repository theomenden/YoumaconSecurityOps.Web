namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetShiftListQueryHandler: IStreamRequestHandler<GetShiftListQuery, ShiftReader>
{
    private readonly ILogger<GetShiftListQueryHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IShiftAccessor _shifts;

    public GetShiftListQueryHandler(ILogger<GetShiftListQueryHandler> logger, IMediator mediator, IShiftAccessor shifts)
    {
        _logger = logger;
        _mediator = mediator;
        _shifts = shifts;
    }
        
    public IAsyncEnumerable<ShiftReader> Handle(GetShiftListQuery request, CancellationToken cancellationToken)
    {
        var shiftLog = _shifts.GetAll(cancellationToken);

        RaiseShiftLogQueriedEvent(request, cancellationToken);

        return shiftLog;
    }

    private Task RaiseShiftLogQueriedEvent(GetShiftListQuery request, CancellationToken cancellationToken)
    {
        var e = new ShiftLogQueriedEvent(null)
        {
            Aggregate = nameof(GetShiftListQuery),
            MajorVersion = 1,
            Name = nameof(ShiftLogQueriedEvent)
        };

        return _mediator.Publish(e, cancellationToken);
    }
}