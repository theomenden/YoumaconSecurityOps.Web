using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Queried;
using YoumaconSecurityOps.Core.EventStore.Storage;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class LocationListQueriedEventHandler: INotificationHandler<LocationListQueriedEvent>
    {
        private readonly IEventStoreRepository _eventStore;

        private readonly ILogger<LocationListQueriedEventHandler> _logger;

        public LocationListQueriedEventHandler(IEventStoreRepository eventStore, ILogger<LocationListQueriedEventHandler> logger)
        {
            _eventStore = eventStore;
            _logger = logger;
        }

        public async Task Handle(LocationListQueriedEvent notification, CancellationToken cancellationToken)
        {
            
            var previousEvents = (await _eventStore.GetAllByAggregateId(notification.AggregateId, cancellationToken)
                .ToListAsync(cancellationToken))
                .AsReadOnly();

            await _eventStore.SaveAsync(notification.AggregateId, notification.MinorVersion, previousEvents,
                notification.Aggregate, cancellationToken);
        }
    }
}
