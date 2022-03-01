namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class ShiftCheckoutCommandHandler : IRequestHandler<ShiftCheckoutCommandWithReturn, Guid>
{
    private readonly IMediator _mediator;

    private readonly IShiftRepository _shifts;

    private readonly ILogger<ShiftReportInCommandHandler> _logger;

    public ShiftCheckoutCommandHandler(IMediator mediator, IShiftRepository shifts, ILogger<ShiftReportInCommandHandler> logger)
    {
        _mediator = mediator;
        _shifts = shifts;
        _logger = logger;   
    }

    public async Task<Guid> Handle(ShiftCheckoutCommandWithReturn request, CancellationToken cancellationToken)
    {
        var updatedShift = await _shifts.CheckOut(request.ShiftId, cancellationToken);

        await RaiseShiftUpdatedEvent(updatedShift, cancellationToken);

        return updatedShift.Id;
    }

    private async Task RaiseShiftUpdatedEvent(ShiftReader updatedShift, CancellationToken cancellationToken)
    {
        var e = new ShiftUpdatedEvent(updatedShift);

        await _mediator.Publish(e, cancellationToken);
    }
}