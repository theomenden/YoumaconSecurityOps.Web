namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class SendOnBreakCommandHandler : IRequestHandler<SendOnBreakCommand, Guid>
{
    private readonly IMediator _mediator;

    private readonly IStaffRepository _staff;

    private readonly ILogger<SendOnBreakCommandHandler> _logger;

    public SendOnBreakCommandHandler(IMediator mediator, IStaffRepository staff, ILogger<SendOnBreakCommandHandler> logger)
    {
        _mediator = mediator;
        _staff = staff;
        _logger = logger;
    }

    public async Task<Guid> Handle(SendOnBreakCommand request, CancellationToken cancellationToken)
    {
        var couldUpdateStaffMember = await _staff.SendOnBreak(request.StaffId, cancellationToken);

        if (couldUpdateStaffMember is null)
        {
            await RaiseFailedToUpdateEntityEvent(request, cancellationToken);

            return Guid.Empty;
        }

        await RaiseStaffMemberUpdatedEvent(couldUpdateStaffMember, cancellationToken);

        return request.StaffId;
    }

    private async Task RaiseStaffMemberUpdatedEvent(StaffReader updatedStaff, CancellationToken cancellationToken)
    {
        var e = new StaffMemberUpdatedEvent(updatedStaff);

        await _mediator.Publish(e, cancellationToken);
    }

    private Task RaiseFailedToUpdateEntityEvent(SendOnBreakCommand command,
        CancellationToken cancellationToken)
    {
        var e = new FailedToUpdateEntityEvent();

        _mediator.Publish(e, cancellationToken);

        return Task.CompletedTask;
    }
}