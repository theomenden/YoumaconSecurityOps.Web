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
using YoumaconSecurityOps.Core.EventStore.Storage;
using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class LocationCreatedEventHandler: INotificationHandler<LocationCreatedEvent>
    {
        private readonly IEventStoreRepository _eventStore;

        private readonly IMapper _mapper;

        private readonly ILocationRepository _locations;

        private readonly IMediator _mediator;

        private readonly ILogger<LocationCreatedEventHandler> _logger;

        public LocationCreatedEventHandler(IEventStoreRepository eventStore, IMapper mapper,ILocationRepository locations, IMediator mediator, ILogger<LocationCreatedEventHandler> logger)
        {
            _eventStore = eventStore;
            _mapper = mapper;
            _locations = locations;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(LocationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var locationEntry = _mapper.Map<LocationReader>(notification.LocationAdded);

            await _locations.AddAsync(locationEntry, cancellationToken);

            await RaiseLocationAddedEvent(notification, locationEntry, cancellationToken);
        }

        private async Task RaiseLocationAddedEvent(LocationCreatedEvent createdLocation, LocationReader locationAdded, CancellationToken cancellationToken)
        {
            var e = new LocationAddedEvent(locationAdded)
            {
                AggregateId = createdLocation.AggregateId,
                Aggregate = createdLocation.Aggregate,
                DataAsJson = locationAdded.ToJson(),
                MajorVersion = createdLocation.MajorVersion,
                MinorVersion = ++createdLocation.MinorVersion,
                Name = createdLocation.Name
            };

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
