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
        var staffWriter = request.StaffWriter;
        var contactWriter = request.ContactWriter;

        await RaiseContactCreatedEvent(contactWriter, cancellationToken)
            .ContinueWith(async (x) => await RaiseStaffCreatedEvent(staffWriter, cancellationToken), cancellationToken);
            
        return staffWriter.Id;
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
}