using TG.Blazor.IndexedDB;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ShiftUpdatedEventHandler: INotificationHandler<ShiftUpdatedEvent>
{
    private readonly IndexedDBManager _indexedDbManager;

    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreContextFactory;

    private readonly IEventStoreRepository _eventStore;

    public ShiftUpdatedEventHandler(IndexedDBManager indexedDbManager, IDbContextFactory<EventStoreDbContext> eventStoreContextFactory, IEventStoreRepository eventStore)
    {
        _indexedDbManager = indexedDbManager;
        _eventStoreContextFactory = eventStoreContextFactory;
        _eventStore = eventStore;
    }

    public async Task Handle(ShiftUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await using var context =
            await _eventStoreContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var previousEvents =  (await _eventStore.GetAllByAggregateIdAsync(context, notification.AggregateId, cancellationToken)).ToList();

        await _eventStore.SaveAsync(context, notification.Id, notification.MajorVersion, nameof(ShiftUpdatedEventHandler) ,previousEvents.AsReadOnly(), notification.Name, cancellationToken);

        await PersistEventToClientStorage(notification)
            .ConfigureAwait(false);
    }

    private async Task PersistEventToClientStorage(ShiftUpdatedEvent eventToStore)
    {
        var eventRecord = new StoreRecord<EventBase>
        {
            Storename = "YsecEvents",
            Data = eventToStore
        };

        await _indexedDbManager.AddRecord(eventRecord).ConfigureAwait(false);
    }
}