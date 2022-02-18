namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class AddContactCommandHandler: IRequestHandler<AddContactCommand>
{
    private readonly IMediator _mediator;

    private readonly ILogger<AddContactCommandHandler> _logger;

    public AddContactCommandHandler(IMediator mediator, ILogger<AddContactCommandHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Unit> Handle(AddContactCommand request, CancellationToken cancellationToken)
    {
        var contactWriter = new ContactWriter(request.CreatedOn, request.Email, request.FirstName, request.LastName,
            request.FacebookName, request.PreferredName, request.PhoneNumber);

        await RaiseContactCreatedEvent(contactWriter, cancellationToken);

        return new Unit();
    }

    private async Task RaiseContactCreatedEvent(ContactWriter contactWriter, CancellationToken cancellationToken)
    {
        var e = new ContactCreatedEvent(contactWriter);

        await _mediator.Publish(e, cancellationToken);
    }
}