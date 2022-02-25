namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ShiftCreatedEventHandler: INotificationHandler<ShiftCreatedEvent>
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly ILogger<ShiftCreatedEventHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IEventStoreRepository _eventStore;

    private readonly IShiftRepository _shifts;

    public ShiftCreatedEventHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, ILogger<ShiftCreatedEventHandler> logger, IMapper mapper, IMediator mediator, IEventStoreRepository eventStore, IShiftRepository shifts)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
        _eventStore = eventStore;
        _shifts = shifts;   
    }

    public async Task Handle(ShiftCreatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var shiftToAdd = _mapper.Map<ShiftReader>(notification.ShiftWriter);

        var shiftWasAddedSuccessfully = await _shifts.AddAsync(context,shiftToAdd, cancellationToken);

        if (!shiftWasAddedSuccessfully)
        {
            await RaiseFailedToAddEntityEvent(shiftToAdd.Id, shiftToAdd.GetType(),cancellationToken);
            return;
        }

        await RaiseShiftAddedEvent(shiftToAdd, cancellationToken);
    }

    private async Task RaiseShiftAddedEvent(ShiftReader shiftAdded, CancellationToken cancellationToken)
    {
        var e = new ShiftAddedEvent(shiftAdded);

        var previousEvents = await _eventStore.GetAllByAggregateId(e.AggregateId, cancellationToken).ToListAsync(cancellationToken);

        await _eventStore.SaveAsync(e.Id, e.MajorVersion, previousEvents.AsReadOnly(), e.Name, cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }

    private async Task RaiseFailedToAddEntityEvent(Guid aggregateId, Type aggregateType, CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(aggregateId, aggregateType);

        var previousEvents = await _eventStore.GetAllByAggregateId(e.AggregateId, cancellationToken).ToListAsync(cancellationToken);

        await _eventStore.SaveAsync(e.Id, e.MajorVersion, previousEvents.AsReadOnly(), e.Name, cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }
}