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
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class LocationCreatedEventHandler: INotificationHandler<LocationCreatedEvent>
    {
        private readonly IMapper _mapper;

        private readonly ILocationRepository _locations;

        private readonly IMediator _mediator;

        private readonly ILogger<LocationCreatedEventHandler> _logger;

        public LocationCreatedEventHandler(IMapper mapper,ILocationRepository locations, IMediator mediator, ILogger<LocationCreatedEventHandler> logger)
        {
            _mapper = mapper;
            _locations = locations;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(LocationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var locationEntry = _mapper.Map<LocationReader>(notification.LocationAdded);

            await _locations.AddAsync(locationEntry, cancellationToken);

            await RaiseLocationAddedEvent(locationEntry);
        }

        private async Task RaiseLocationAddedEvent(LocationReader locationAdded)
        {
            var e = new LocationAddedEvent(locationAdded);

            await _mediator.Publish(e);
        }
    }
}
