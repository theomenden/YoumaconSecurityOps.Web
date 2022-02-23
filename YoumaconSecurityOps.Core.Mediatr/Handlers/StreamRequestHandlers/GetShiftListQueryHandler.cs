
namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetShiftListQueryHandler: IStreamRequestHandler<GetShiftListQuery, ShiftReader>
{
    private readonly ILogger<GetShiftListQueryHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IShiftAccessor _shifts;
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetShiftListQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, ILogger<GetShiftListQueryHandler> logger, IMediator mediator, IShiftAccessor shifts)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
        _mediator = mediator;
        _shifts = shifts;
    }
        
    public async IAsyncEnumerable<ShiftReader> Handle(GetShiftListQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        await foreach (var shift in _shifts.GetAllAsync(context, cancellationToken).ConfigureAwait(false))
        {
            yield return shift;
        }

        await RaiseShiftLogQueriedEvent(request, cancellationToken);
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