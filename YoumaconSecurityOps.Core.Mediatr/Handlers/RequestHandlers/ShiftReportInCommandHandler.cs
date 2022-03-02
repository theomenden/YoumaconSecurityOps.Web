namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class ShiftReportInCommandHandler : IRequestHandler<ShiftReportInCommandWithReturn, Guid>
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly IMediator _mediator;

    private readonly IShiftRepository _shifts;

    private readonly ILogger<ShiftReportInCommandHandler> _logger;

    public ShiftReportInCommandHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IMediator mediator, IShiftRepository shifts, ILogger<ShiftReportInCommandHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _mediator = mediator;
        _shifts = shifts;
        _logger = logger;
    }

    public async Task<Guid> Handle(ShiftReportInCommandWithReturn request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);
        

        var updatedShift = await _shifts.ReportIn(context, request.ShiftId, request.CurrentLocationId, cancellationToken);

        await RaiseShiftUpdatedEvent(updatedShift, cancellationToken);

        return updatedShift.Id;
    }

    private async Task RaiseShiftUpdatedEvent(ShiftReader updatedShift, CancellationToken cancellationToken)
    {
        var e = new ShiftUpdatedEvent(updatedShift);

        await _mediator.Publish(e, cancellationToken);
    }
}