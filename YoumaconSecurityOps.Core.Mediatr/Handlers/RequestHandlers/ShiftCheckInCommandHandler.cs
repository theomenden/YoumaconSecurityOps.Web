namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class ShiftCheckInCommandHandler : IRequestHandler<ShiftCheckInCommandWithReturn, Guid>
{
    private readonly IMediator _mediator;

    private readonly IShiftRepository _shifts;

    private readonly ILogger<ShiftReportInCommandHandler> _logger;

    public ShiftCheckInCommandHandler(IMediator mediator, IShiftRepository shifts, ILogger<ShiftReportInCommandHandler> logger)
    {
        _mediator = mediator;
        _shifts = shifts;
        _logger = logger;   
    }

    public async Task<Guid> Handle(ShiftCheckInCommandWithReturn request, CancellationToken cancellationToken)
    {
        var updatedShift = await _shifts.CheckIn(request.ShiftId, cancellationToken);

        await RaiseShiftUpdatedEvent(updatedShift, cancellationToken);

        return updatedShift.Id;
    }

    private async Task RaiseShiftUpdatedEvent(ShiftReader updatedShift, CancellationToken cancellationToken)
    {
        var e = new ShiftUpdatedEvent(updatedShift);

        await _mediator.Publish(e, cancellationToken);
    }
}