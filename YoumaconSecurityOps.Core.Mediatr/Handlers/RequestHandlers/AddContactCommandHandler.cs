namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class AddContactCommandHandler: IRequestHandler<AddContactCommand,Guid>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    public AddContactCommandHandler(IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory, IEventStoreRepository eventStore, IMapper mapper, IMediator mediator)
    {
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
        _eventStore = eventStore;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(AddContactCommand request, CancellationToken cancellationToken)
    {
        var contactWriter = request.ContactInformation;

        await RaiseContactCreatedEvent(contactWriter, cancellationToken);

        return contactWriter.Id;
    }

    private async Task RaiseContactCreatedEvent(ContactWriter contactWriter, CancellationToken cancellationToken)
    {
        var e = new ContactCreatedEvent(contactWriter)
        {
            Name = nameof(AddContactCommand)
        };

        await using var context = await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var mappedEvent = _mapper.Map<EventReader>(e);

        await _eventStore.SaveAsync(context, mappedEvent, cancellationToken).ConfigureAwait(false);

        await _mediator.Publish(e, cancellationToken);
    }
}