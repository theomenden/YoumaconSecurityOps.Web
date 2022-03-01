namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal class StaffTypeRoleMapAddedEventHandler : INotificationHandler<StaffTypeRoleMapAddedEvent>
{

    private readonly IEventStoreRepository _eventStore;

    private readonly ILogger<StaffTypeRoleMapAddedEventHandler> _logger;

    public StaffTypeRoleMapAddedEventHandler(IMapper mapper, IEventStoreRepository eventStore, ILogger<StaffTypeRoleMapAddedEventHandler> logger)
    {
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task Handle(StaffTypeRoleMapAddedEvent notification, CancellationToken cancellationToken)
    {
        var eventsOnThisAggregate = (await _eventStore
                .GetAllByAggregateId(notification.Id, cancellationToken)
                .ToListAsync(cancellationToken))
            .AsReadOnly();

        await _eventStore.SaveAsync(notification.Id, notification.MinorVersion, eventsOnThisAggregate, notification.Name, cancellationToken);
    }
}

