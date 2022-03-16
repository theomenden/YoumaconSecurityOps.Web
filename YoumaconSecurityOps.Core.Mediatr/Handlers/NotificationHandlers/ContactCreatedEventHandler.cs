namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ContactCreatedEventHandler : INotificationHandler<ContactCreatedEvent>
{
    private readonly IContactRepository _contacts;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<ContactCreatedEventHandler> _logger;

    public ContactCreatedEventHandler(IContactRepository contacts, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory, IEventStoreRepository eventStore, IMapper mapper, IMediator mediator, ILogger<ContactCreatedEventHandler> logger)
    {
        _contacts = contacts;
        _dbContextFactory = dbContextFactory;
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
        _eventStore = eventStore;
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(ContactCreatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var contactToAdd = _mapper.Map<ContactReader>(notification.ContactWriter);

        var couldAdd = await _contacts.AddAsync(context, contactToAdd, cancellationToken);

        if (!couldAdd)
        {
            await RaiseFailedToAddEntityEvent(notification, cancellationToken);
        }

        await RaiseContactAddedEvent(notification, contactToAdd, cancellationToken);
    }

    private async Task RaiseContactAddedEvent(ContactCreatedEvent notification, ContactReader contactReader, CancellationToken cancellationToken)
    {
        var e = new ContactAddedEvent(contactReader)
        {
            Aggregate = notification.Aggregate,
            AggregateId = notification.AggregateId
        };

        await using var context = await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var previousEvents = (await _eventStore.GetAllByAggregateIdAsync(context, notification.AggregateId, cancellationToken)).ToList();

        await _eventStore.SaveAsync(context, e.AggregateId, notification.MinorVersion, nameof(RaiseContactAddedEvent),
            previousEvents.AsReadOnly(), notification.Aggregate, cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }

    private async Task RaiseFailedToAddEntityEvent(ContactCreatedEvent @event, CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(@event.AggregateId, @event.GetType());

        await _mediator.Publish(e, cancellationToken);
    }
}