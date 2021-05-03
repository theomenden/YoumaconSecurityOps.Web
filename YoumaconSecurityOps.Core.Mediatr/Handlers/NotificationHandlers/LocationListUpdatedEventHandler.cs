using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using YoumaconSecurityOps.Core.EventStore.Events;
using YoumaconSecurityOps.Core.EventStore.Events.Updated;
using YoumaconSecurityOps.Core.EventStore.Storage;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class LocationListUpdatedEventHandler: INotificationHandler<LocationListUpdatedEvent>
    {
        private readonly IEventStoreRepository _eventStore;

        private readonly ILogger<LocationListUpdatedEvent> _logger;

        private readonly IMapper _mapper;

        public LocationListUpdatedEventHandler(IEventStoreRepository eventStore, IMapper mapper, ILogger<LocationListUpdatedEvent> logger)
        {
            _eventStore = eventStore;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Handle(LocationListUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var eventToAdd = _mapper.Map<EventReader>(notification);

            var previousEvents = await _eventStore.GetAllByAggregateId(eventToAdd.AggregateId, cancellationToken).ToListAsync(cancellationToken);

            previousEvents.Add(eventToAdd);

            await _eventStore.SaveAsync(eventToAdd.AggregateId, eventToAdd.MinorVersion, previousEvents.AsReadOnly(), eventToAdd.Aggregate,
                cancellationToken);
        }
    }
}
