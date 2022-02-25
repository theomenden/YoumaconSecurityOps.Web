namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class AddFullStaffRequestCommandHandler : IRequestHandler<AddFullStaffEntryCommand, Guid>
{
    private readonly IMediator _mediator;

    private readonly ILogger<AddFullStaffRequestCommandHandler> _logger;

    public AddFullStaffRequestCommandHandler(IMediator mediator, ILogger<AddFullStaffRequestCommandHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Guid> Handle(AddFullStaffEntryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await RaiseStaffCreatedEvent(request.StaffWriter, cancellationToken);

            await RaiseContactCreatedEvent(request.ContactWriter, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Could not handle request {@request}: exception {@ex}", request,ex);
            throw;
        }

        return request.StaffWriter.Id;
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

    private async Task RaiseFailedToCreateEntityEvent(AddFullStaffEntryCommand request, CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(request.Id, typeof(AddFullStaffEntryCommand));

        await _mediator.Publish(e, cancellationToken);
    }
}