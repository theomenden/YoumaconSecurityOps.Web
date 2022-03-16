using TG.Blazor.IndexedDB;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ShiftCreatedEventHandler : INotificationHandler<ShiftCreatedEvent>
{
    private readonly IndexedDBManager _indexedDbManager;

    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly ILogger<ShiftCreatedEventHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IEventStoreRepository _eventStore;

    private readonly IShiftRepository _shifts;

    public ShiftCreatedEventHandler(IndexedDBManager indexedDbManager,
        IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory,
        IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, ILogger<ShiftCreatedEventHandler> logger,
        IMapper mapper, IMediator mediator, IEventStoreRepository eventStore, IShiftRepository shifts)
    {
        _indexedDbManager = indexedDbManager;
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
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

        var shiftWasAddedSuccessfully = await _shifts.AddAsync(context, shiftToAdd, cancellationToken);

        if (!shiftWasAddedSuccessfully)
        {
            await RaiseFailedToAddEntityEvent(shiftToAdd.Id, shiftToAdd.GetType(), cancellationToken);
            return;
        }

        await RaiseShiftAddedEvent(notification, shiftToAdd, cancellationToken);
    }

    private async Task RaiseShiftAddedEvent(ShiftCreatedEvent previousEvent, ShiftReader shiftAdded, CancellationToken cancellationToken)
    {
        var e = new ShiftAddedEvent(shiftAdded)
        {
            Aggregate = previousEvent.Aggregate,
            AggregateId = previousEvent.AggregateId,
            Name = nameof(ShiftCreatedEventHandler)
        };

        await using var context =
            await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var previousEvents = await _eventStore.GetAllByAggregateId(context, previousEvent.AggregateId, cancellationToken)
            .ToListAsync(cancellationToken);

        await _eventStore.SaveAsync(context, e.AggregateId, previousEvent.MinorVersion, nameof(ShiftCreatedEventHandler), previousEvents.AsReadOnly(), e.Aggregate,
            cancellationToken);


        await _mediator.Publish(e, cancellationToken);
    }

    private async Task RaiseFailedToAddEntityEvent(Guid aggregateId, Type aggregateType,
        CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(aggregateId, aggregateType);

        await using var context =
            await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var previousEvents = await _eventStore.GetAllByAggregateId(context, e.AggregateId, cancellationToken)
            .ToListAsync(cancellationToken);

        await _eventStore.SaveAsync(context, e.Id, e.MajorVersion, nameof(ShiftCreatedEventHandler), previousEvents.AsReadOnly(), e.Name,
            cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }


}