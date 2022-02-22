namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetRadioScheduleQueryHandler : IStreamRequestHandler<GetRadioSchedule, RadioScheduleReader>
{
    private readonly IRadioScheduleAccessor _radios;

    private readonly IMediator _mediator;

    private readonly ILogger<GetRadioScheduleQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;
    public GetRadioScheduleQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IRadioScheduleAccessor radios, IMediator mediator, ILogger<GetRadioScheduleQueryHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _radios = radios;
        _mediator = mediator;
        _logger = logger;
    }

    public IAsyncEnumerable<RadioScheduleReader> Handle(GetRadioSchedule request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var radios = _radios.GetAllAsync(context, cancellationToken);

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