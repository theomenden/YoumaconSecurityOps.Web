namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class ReturnFromBreakCommandHandler : IRequestHandler<ReturnFromBreakCommandWithReturn, Guid>
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly IMediator _mediator;

    private readonly IStaffRepository _staff;

    private readonly ILogger<ReturnFromBreakCommandHandler> _logger;

    public ReturnFromBreakCommandHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IMediator mediator, IStaffRepository staff, ILogger<ReturnFromBreakCommandHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _mediator = mediator;
        _staff = staff;
        _logger = logger;
    }

    public async Task<Guid> Handle(ReturnFromBreakCommandWithReturn request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var couldUpdateStaffMember = await _staff.ReturnFromBreak(context, request.StaffId, cancellationToken);

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

    private Task RaiseFailedToUpdateEntityEvent(ReturnFromBreakCommandWithReturn commandWithReturn,
        CancellationToken cancellationToken)
    {
        var e = new FailedToUpdateEntityEvent();

        _mediator.Publish(e, cancellationToken);

        return Task.CompletedTask;
    }
}