namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class StaffCreatedEventHandler : INotificationHandler<StaffCreatedEvent>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly IStaffRepository _staff;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<StaffCreatedEventHandler> _logger;

    public StaffCreatedEventHandler(IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory, IEventStoreRepository eventStore, IStaffRepository staff, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IMapper mapper, IMediator mediator, ILogger<StaffCreatedEventHandler> logger)
    {
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
        _eventStore = eventStore;
        _staff = staff;
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(StaffCreatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var staffMember = notification.StaffWriter.FromFullStaff();
        
        if(!(await _staff.AddAsync(context, staffMember, cancellationToken)))
        {
            _logger.LogError("Failed to add staff information for member with id {staffId}", staffMember.Id);
            await RaiseFailedToAddEntityEvent(notification, cancellationToken);
            return;
        }
        
        await RaiseStaffListUpdatedEvent(staffMember, cancellationToken);
    }

    private async Task RaiseStaffListUpdatedEvent(StaffReader staffReader, CancellationToken cancellationToken)
    {
        var e = new StaffListUpdatedEvent(staffReader)
        {
            Name = nameof(StaffListUpdatedEvent)
        };

        await using var context = await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await _eventStore.ApplyNextEventAsync(context, _mapper.Map<EventReader>(e), cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }
    private async Task RaiseFailedToAddEntityEvent(StaffCreatedEvent staffCreatedEvent, CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(staffCreatedEvent.AggregateId, staffCreatedEvent.GetType());

        await _mediator.Publish(e, cancellationToken);
    }
}