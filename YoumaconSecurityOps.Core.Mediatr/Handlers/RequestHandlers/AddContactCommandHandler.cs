namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class AddContactCommandHandler: IRequestHandler<AddContactCommand,Guid>
{
    private readonly IMediator _mediator;

    private readonly ILogger<AddContactCommandHandler> _logger;

    public AddContactCommandHandler(IMediator mediator, ILogger<AddContactCommandHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Guid> Handle(AddContactCommand request, CancellationToken cancellationToken)
    {
        var contactWriter = request.ContactInformation;

        await RaiseContactCreatedEvent(contactWriter, cancellationToken);

        return contactWriter.Id;
    }

    private async Task RaiseContactCreatedEvent(ContactWriter contactWriter, CancellationToken cancellationToken)
    {
        var e = new ContactCreatedEvent(contactWriter);

        await _mediator.Publish(e, cancellationToken);
    }
}