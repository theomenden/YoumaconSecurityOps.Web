namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class ShiftReportInCommandHandler : IRequestHandler<ShiftReportInCommandWithReturn, Guid>
{
    private readonly IMediator _mediator;

    private readonly IShiftRepository _shifts;

    private readonly ILogger<ShiftReportInCommandHandler> _logger;
        
    public ShiftReportInCommandHandler(IMediator mediator, ILogger<ShiftReportInCommandHandler> logger, IShiftRepository shifts)
    {
        _mediator = mediator;

        _logger = logger;

        _shifts = shifts;
    }

    public async Task<Guid> Handle(ShiftReportInCommandWithReturn request, CancellationToken cancellationToken)
    {
        var updatedShift = await _shifts.ReportIn(request.ShiftId, request.CurrentLocationId, cancellationToken);

        await RaiseShiftUpdatedEvent(updatedShift, cancellationToken);

        return updatedShift.Id;
    }

    private async Task RaiseShiftUpdatedEvent(ShiftReader updatedShift, CancellationToken cancellationToken)
    {
        var e = new ShiftUpdatedEvent(updatedShift);

        await _mediator.Publish(e, cancellationToken);
    }
}