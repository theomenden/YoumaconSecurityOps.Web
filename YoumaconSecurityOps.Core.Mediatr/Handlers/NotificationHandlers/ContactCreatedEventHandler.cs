namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ContactCreatedEventHandler : INotificationHandler<ContactCreatedEvent>
{
    private readonly IContactRepository _contacts;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<ContactCreatedEventHandler> _logger;

    public ContactCreatedEventHandler(IContactRepository contacts, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IMapper mapper, IMediator mediator, ILogger<ContactCreatedEventHandler> logger)
    {
        _contacts = contacts;
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(ContactCreatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var contactToAdd = _mapper.Map<ContactReader>(notification.ContactWriter);

        try
        {
            await _contacts.AddAsync(context, contactToAdd, cancellationToken);

            await RaiseContactAddedEvent(contactToAdd, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Could not add contact with Id: {contactId} to database: {@ex}", contactToAdd.Id, ex);
            
            await RaiseFailedToAddEntityEvent(notification, cancellationToken);
            throw;
        }
    }

    private async Task RaiseContactAddedEvent(ContactReader contactReader, CancellationToken cancellationToken)
    {
        var e = new ContactAddedEvent(contactReader);

        await _mediator.Publish(e, cancellationToken);
    }

    private async Task RaiseFailedToAddEntityEvent(ContactCreatedEvent @event, CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(@event.AggregateId, @event.GetType());

        await _mediator.Publish(e, cancellationToken);
    }
}