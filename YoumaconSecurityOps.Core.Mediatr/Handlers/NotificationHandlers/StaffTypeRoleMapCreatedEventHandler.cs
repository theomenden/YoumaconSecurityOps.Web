namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal class StaffTypeRoleMapCreatedEventHandler : INotificationHandler<StaffTypeRoleMapCreatedEvent>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly ILogger<StaffTypeRoleMapCreatedEventHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IStaffRoleMapRepository _staffRoleMaps;

    public StaffTypeRoleMapCreatedEventHandler(IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory, IEventStoreRepository eventStore, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, ILogger<StaffTypeRoleMapCreatedEventHandler> logger, IMapper mapper, IMediator mediator, IStaffRoleMapRepository staffRoleMaps)
    {
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
        _eventStore = eventStore;
        _dbContextFactory = dbContextFactory;
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
        _staffRoleMaps = staffRoleMaps;
    }

    public async Task Handle(StaffTypeRoleMapCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

            var staffTypeRoleMap = _mapper.Map<StaffTypesRoles>(notification.StaffTypeRoleMapWriter);

            await _staffRoleMaps.AddAsync(context, staffTypeRoleMap, cancellationToken);

            await RaiseStaffTypeRoleMapAddedEvent(notification, staffTypeRoleMap, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception thrown: {@ex}", e);
            await RaiseFailedToAddEntityEvent(notification, cancellationToken);
        }
    }

    private async Task RaiseFailedToAddEntityEvent(StaffTypeRoleMapCreatedEvent failedEvent, CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(failedEvent.AggregateId, failedEvent.GetType());

        await _mediator.Publish(e, cancellationToken);
    }

    private async Task RaiseStaffTypeRoleMapAddedEvent(StaffTypeRoleMapCreatedEvent previousEvent, StaffTypesRoles staffTypeRoleMap,
        CancellationToken cancellationToken)
    {
        var e = new StaffTypeRoleMapAddedEvent(staffTypeRoleMap)
        {
            Name = nameof(StaffTypeRoleMapCreatedEvent)
        };
        
        await using var context =
            await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

       var previousEvents = (await _eventStore.GetAllByAggregateIdAsync(context, previousEvent.AggregateId, cancellationToken)).ToList();

       await _eventStore.SaveAsync(context, previousEvent.AggregateId, previousEvent.MinorVersion,
           nameof(RaiseStaffTypeRoleMapAddedEvent),
           previousEvents.AsReadOnly(), previousEvent.Aggregate, cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }
}

