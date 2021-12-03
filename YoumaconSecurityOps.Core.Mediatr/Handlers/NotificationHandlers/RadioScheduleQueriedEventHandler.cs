using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events;
using YoumaconSecurityOps.Core.EventStore.Events.Queried;
using YoumaconSecurityOps.Core.EventStore.Storage;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class RadioScheduleQueriedEventHandler : INotificationHandler<RadioScheduleQueriedEvent>
    {
        private readonly IEventStoreRepository _eventStore;

        private readonly IMediator _mediator;

        private ILogger<RadioScheduleQueriedEventHandler> _logger;

        public RadioScheduleQueriedEventHandler(IEventStoreRepository eventStore, IMediator mediator,
            ILogger<RadioScheduleQueriedEventHandler> logger)
        {
            _eventStore = eventStore;
            _mediator = mediator;
            _logger = logger;
        }


        public async Task Handle(RadioScheduleQueriedEvent notification, CancellationToken cancellationToken)
        {
            var @event = new EventReader
            {
                Aggregate = notification.Aggregate,
                Data = notification.DataAsJson,
                MajorVersion = notification.MajorVersion,
                MinorVersion = notification.MinorVersion,
                Name = notification.Name
            };

            var previousEventsOnAggregate = new List<EventReader> { @event };

            await _eventStore.SaveAsync(notification.Id, notification.MinorVersion,
                previousEventsOnAggregate.ToList().AsReadOnly(),
                notification.Aggregate, cancellationToken);
        }
    }
}
