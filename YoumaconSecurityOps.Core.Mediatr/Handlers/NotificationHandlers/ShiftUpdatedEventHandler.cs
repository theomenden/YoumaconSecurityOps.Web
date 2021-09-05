using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Updated;
using YoumaconSecurityOps.Core.EventStore.Storage;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class ShiftUpdatedEventHandler: INotificationHandler<ShiftUpdatedEvent>
    {
        private readonly IEventStoreRepository _eventStore;

        private readonly ILogger<ShiftUpdatedEventHandler> _logger;

        public ShiftUpdatedEventHandler(IEventStoreRepository eventStore, ILogger<ShiftUpdatedEventHandler> logger)
        {
            _eventStore = eventStore;
            _logger = logger;
        }

        public async Task Handle(ShiftUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var previousEvents =  (await _eventStore.GetAllByAggregateIdAsync(notification.Id, cancellationToken)).ToList();

            await _eventStore.SaveAsync(notification.Id, notification.MajorVersion, previousEvents.AsReadOnly(), notification.Name, cancellationToken);
        }
    }
}
