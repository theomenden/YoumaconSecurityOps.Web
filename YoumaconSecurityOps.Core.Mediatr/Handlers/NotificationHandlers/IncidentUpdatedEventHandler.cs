using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Updated;
using YoumaconSecurityOps.Core.EventStore.Storage;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class IncidentUpdatedEventHandler : INotificationHandler<IncidentUpdatedEvent>
    {
        private readonly IEventStoreRepository _eventStore;

        private readonly ILogger<IncidentUpdatedEventHandler> _logger;

        public IncidentUpdatedEventHandler(IEventStoreRepository eventStore, ILogger<IncidentUpdatedEventHandler> logger)
        {
            _eventStore = eventStore;
            _logger = logger;
        }

        public async Task Handle(IncidentUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var previousEvents = (await _eventStore.GetAllByAggregateIdAsync(notification.Id, cancellationToken)).ToList();

            await _eventStore.SaveAsync(notification.Id, notification.MajorVersion, previousEvents.AsReadOnly(), notification.Name, cancellationToken);
        }
    }
}
