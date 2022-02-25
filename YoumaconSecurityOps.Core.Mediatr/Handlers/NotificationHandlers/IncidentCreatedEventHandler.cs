namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class IncidentCreatedEventHandler : INotificationHandler<IncidentCreatedEvent>
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly ILogger<IncidentCreatedEventHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IEventStoreRepository _eventStore;

    private readonly IIncidentRepository _incidents;

    public IncidentCreatedEventHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, ILogger<IncidentCreatedEventHandler> logger, IMapper mapper, IMediator mediator, IEventStoreRepository eventStore, IIncidentRepository incidents)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
        _eventStore = eventStore;
        _incidents = incidents;
    }

    public async Task Handle(IncidentCreatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var incidentToAdd = _mapper.Map<IncidentReader>(notification.IncidentWriter);

        var incidentWasAddedSuccessfully = await _incidents.AddAsync(context,incidentToAdd, cancellationToken);

        if (!incidentWasAddedSuccessfully)
        {
            await RaiseFailedToAddEntityEvent(incidentToAdd.Id, incidentToAdd.GetType(), cancellationToken);
            return;
        }

        await RaiseIncidentAddEvent(incidentToAdd, cancellationToken);
    }

    private async Task RaiseIncidentAddEvent(IncidentReader incidentAdded, CancellationToken cancellationToken)
    {
        var e = new IncidentAddedEvent(incidentAdded);

        var previousEvents = await _eventStore.GetAllByAggregateId(e.AggregateId, cancellationToken)
            .ToListAsync(cancellationToken);

        await _eventStore.SaveAsync(e.Id, e.MajorVersion, previousEvents.AsReadOnly(), e.Name, cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }

    private async Task RaiseFailedToAddEntityEvent(Guid aggregateId, Type aggregateType,
        CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(aggregateId, aggregateType);

        var previousEvents = await _eventStore.GetAllByAggregateId(e.AggregateId, cancellationToken)
            .ToListAsync(cancellationToken);

        await _eventStore.SaveAsync(e.Id, e.MajorVersion, previousEvents.AsReadOnly(), e.Name, cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }
}