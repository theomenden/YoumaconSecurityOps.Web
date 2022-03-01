namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal class StaffTypeRoleMapCreatedEventHandler : INotificationHandler<StaffTypeRoleMapCreatedEvent>
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    private readonly ILogger<StaffTypeRoleMapCreatedEventHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IStaffRoleMapRepository _staffRoleMaps;

    public StaffTypeRoleMapCreatedEventHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, ILogger<StaffTypeRoleMapCreatedEventHandler> logger,
        IMapper mapper, IMediator mediator, IStaffRoleMapRepository staffRoleMaps)
    {
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

            await RaiseStaffTypeRoleMapAddedEvent(staffTypeRoleMap, cancellationToken);
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

    private async Task RaiseStaffTypeRoleMapAddedEvent(StaffTypesRoles staffTypeRoleMap,
        CancellationToken cancellationToken)
    {
        var e = new StaffTypeRoleMapAddedEvent(staffTypeRoleMap);

        await _mediator.Publish(e, cancellationToken);
    }
}

