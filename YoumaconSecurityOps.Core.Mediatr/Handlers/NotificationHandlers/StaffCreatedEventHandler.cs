namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class StaffCreatedEventHandler : INotificationHandler<StaffCreatedEvent>
{
    private readonly IStaffRepository _staff;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<StaffCreatedEventHandler> _logger;

    public StaffCreatedEventHandler(IStaffRepository staff, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IMapper mapper, IMediator mediator, ILogger<StaffCreatedEventHandler> logger)
    {
        _staff = staff;
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(StaffCreatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var staffMember = _mapper.Map<StaffReader>(notification.StaffWriter);
        
        try
        {
            await _staff.AddAsync(context, staffMember, cancellationToken);

            await RaiseStaffListUpdatedEvent(staffMember, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to add staff information for member with id {staffId} exception: {@ex}", staffMember.Id, ex);
            await RaiseFailedToAddEntityEvent(notification, cancellationToken);
            throw;
        }
    }

    private async Task RaiseStaffListUpdatedEvent(StaffReader staffReader, CancellationToken cancellationToken)
    {
        var e = new StaffListUpdatedEvent(staffReader);

        await _mediator.Publish(e, cancellationToken);
    }
    private async Task RaiseFailedToAddEntityEvent(StaffCreatedEvent staffCreatedEvent, CancellationToken cancellationToken)
    {
        var e = new FailedToAddEntityEvent(staffCreatedEvent.AggregateId, staffCreatedEvent.GetType());

        await _mediator.Publish(e, cancellationToken);
    }
}