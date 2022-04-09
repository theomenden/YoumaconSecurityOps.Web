namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal class StaffListUpdatedEventHandler: INotificationHandler<StaffListUpdatedEvent>
{
    private readonly IEventStoreRepository _eventStore;
    
    private readonly IDbContextFactory<EventStoreDbContext> _dbContextFactory;

    private readonly IMapper _mapper;

    public StaffListUpdatedEventHandler(IEventStoreRepository eventStore, IDbContextFactory<EventStoreDbContext> dbContextFactory, IMapper mapper)
    {
        _eventStore = eventStore;
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task Handle(StaffListUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);
        
        var e = _mapper.Map<EventReader>(notification);

        await _eventStore.ApplyNextEventAsync(context, e, cancellationToken);
    }
}

