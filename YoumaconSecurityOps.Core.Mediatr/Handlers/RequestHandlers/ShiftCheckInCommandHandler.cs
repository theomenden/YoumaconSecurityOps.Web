namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class ShiftCheckInCommandHandler : IRequestHandler<ShiftCheckInCommandWithReturn, Guid>
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly IMediator _mediator;

    private readonly IShiftRepository _shifts;

    private readonly ILogger<ShiftReportInCommandHandler> _logger;

    public ShiftCheckInCommandHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IMediator mediator, IShiftRepository shifts, ILogger<ShiftReportInCommandHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _mediator = mediator;
        _shifts = shifts;
        _logger = logger;
    }

    public async Task<Guid> Handle(ShiftCheckInCommandWithReturn request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var updatedShift = await _shifts.CheckIn(context,request.ShiftId, cancellationToken);

        await RaiseShiftUpdatedEvent(updatedShift, cancellationToken);

        return updatedShift.Id;
    }

    private async Task RaiseShiftUpdatedEvent(ShiftReader updatedShift, CancellationToken cancellationToken)
    {
        var e = new ShiftUpdatedEvent(updatedShift);

        await _mediator.Publish(e, cancellationToken);
    }
}