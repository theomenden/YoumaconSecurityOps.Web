namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class AddFullStaffRequestCommandHandler : IRequestHandler<AddFullStaffEntryCommandWithReturn, Guid>
{
    private readonly IMediator _mediator;

    private readonly ILogger<AddFullStaffRequestCommandHandler> _logger;

    public AddFullStaffRequestCommandHandler(IMediator mediator, ILogger<AddFullStaffRequestCommandHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Guid> Handle(AddFullStaffEntryCommandWithReturn request, CancellationToken cancellationToken)
    {
        try
        {
            await RaiseStaffCreatedEvent(request.StaffWriter, cancellationToken);

            var staffTypeRoleMap = new StaffTypeRoleMapWriter(request.StaffWriter.Id, request.StaffWriter.StaffTypeId,
                request.StaffWriter.RoleId);

            await RaiseStaffTypeRoleMapCreatedEvent(staffTypeRoleMap, cancellationToken);

            await RaiseContactCreatedEvent(request.ContactWriter, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Could not handle request {@request}: exception {@ex}", request, ex);
            throw;
        }

        return request.StaffWriter.Id;
    }

    private async Task RaiseStaffTypeRoleMapCreatedEvent(StaffTypeRoleMapWriter staffTypeRoleMapWriter, CancellationToken cancellationToken)
    {
        var e = new StaffTypeRoleMapCreatedEvent(staffTypeRoleMapWriter);

        await _mediator.Publish(e, cancellationToken);
    }

    private async Task RaiseStaffCreatedEvent(StaffWriter staffWriter, CancellationToken cancellationToken)
    {
        var e = new StaffCreatedEvent(staffWriter);

        await _mediator.Publish(e, cancellationToken);
    }

    private async Task RaiseContactCreatedEvent(ContactWriter contactWriter, CancellationToken cancellationToken)
    {
        var e = new ContactCreatedEvent(contactWriter);

        await _mediator.Publish(e, cancellationToken);
    }

    private async Task RaiseFailedToCreateEntityEvent(AddFullStaffEntryCommandWithReturn request, CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(request.Id, typeof(AddFullStaffEntryCommandWithReturn));

        await _mediator.Publish(e, cancellationToken);
    }
}