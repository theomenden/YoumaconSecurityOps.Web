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
using YoumaconSecurityOps.Core.EventStore.Events.Failed;
using YoumaconSecurityOps.Core.EventStore.Storage;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class IncidentCreatedEventHandler : INotificationHandler<IncidentCreatedEvent>
    {
        private readonly ILogger<IncidentCreatedEventHandler> _logger;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        private readonly IEventStoreRepository _eventStore;

        private readonly IIncidentRepository _incidents;

        public IncidentCreatedEventHandler(ILogger<IncidentCreatedEventHandler> logger, IMapper mapper, IMediator mediator,
            IEventStoreRepository eventStore, IIncidentRepository incidents)
        {
            _logger = logger;

            _mapper = mapper;

            _mediator = mediator;

            _eventStore = eventStore;

            _incidents = incidents;
        }

        public async Task Handle(IncidentCreatedEvent notification, CancellationToken cancellationToken)
        {
            var incidentToAdd = _mapper.Map<IncidentReader>(notification.IncidentWriter);

            var incidentWasAddedSuccessfully = await _incidents.AddAsync(incidentToAdd, cancellationToken);

            if (!incidentWasAddedSuccessfully)
            {
                await RaiseFailedToAddEntityEvent(incidentToAdd.Id, incidentToAdd.GetType(), cancellationToken);
                return;
            }

            await RaiseIncidentAddEvent(incidentToAdd, cancellationToken);
        }

        private async Task RaiseIncidentAddEvent(IncidentReader incidentAdded, CancellationToken cancellationToken)
        {
            var e = new IncidentAddedEvent(incidentAdded);

            var previousEvents = await _eventStore.GetAllByAggregateId(e.AggregateId, cancellationToken)
                .ToListAsync(cancellationToken);

            await _eventStore.SaveAsync(e.Id, e.MajorVersion, previousEvents.AsReadOnly(), e.Name, cancellationToken);

            await _mediator.Publish(e, cancellationToken);
        }

        private async Task RaiseFailedToAddEntityEvent(Guid aggregateId, Type aggregateType,
            CancellationToken cancellationToken)
        {
            var e = new FailedToAddEntityEvent(aggregateId, aggregateType);

            var previousEvents = await _eventStore.GetAllByAggregateId(e.AggregateId, cancellationToken)
                .ToListAsync(cancellationToken);

            await _eventStore.SaveAsync(e.Id, e.MajorVersion, previousEvents.AsReadOnly(), e.Name, cancellationToken);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
