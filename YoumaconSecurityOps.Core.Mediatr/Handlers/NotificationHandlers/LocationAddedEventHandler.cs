using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Added;
using YoumaconSecurityOps.Core.EventStore.Events.Created;
using YoumaconSecurityOps.Core.EventStore.Events.Updated;
using YoumaconSecurityOps.Core.EventStore.Storage;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class LocationAddedEventHandler : INotificationHandler<LocationAddedEvent>
    {
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        private readonly ILogger<LocationAddedEventHandler> _logger;

        public LocationAddedEventHandler(IEventStoreRepository eventStore, IMapper mapper, IMediator mediator, ILogger<LocationAddedEventHandler> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(LocationAddedEvent notification, CancellationToken cancellationToken)
        {
            await RaiseLocationCreatedEvent(notification, cancellationToken);
        }

        private async Task RaiseLocationCreatedEvent(LocationAddedEvent addedEvent, CancellationToken cancellationToken)
        {
            var e = new LocationCreatedEvent(addedEvent.LocationAdded)
            {
                AggregateId = addedEvent.AggregateId,
                Aggregate = addedEvent.Aggregate,
                MajorVersion = addedEvent.MajorVersion,
                MinorVersion = ++addedEvent.MinorVersion,
                Name = addedEvent.Name
            };

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
