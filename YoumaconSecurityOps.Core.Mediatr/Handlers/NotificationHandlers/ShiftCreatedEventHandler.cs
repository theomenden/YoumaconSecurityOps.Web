using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Added;
using YoumaconSecurityOps.Core.EventStore.Events.Created;
using YoumaconSecurityOps.Core.EventStore.Storage;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class ShiftCreatedEventHandler: INotificationHandler<ShiftCreatedEvent>
    {
        private readonly ILogger<ShiftCreatedEventHandler> _logger;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        private readonly IEventStoreRepository _eventStore;

        private readonly IShiftRepository _shifts;

        public ShiftCreatedEventHandler(ILogger<ShiftCreatedEventHandler> logger, IMapper mapper, IMediator mediator, IEventStoreRepository eventStore, IShiftRepository shifts)
        {
            _logger = logger;

            _mapper = mapper;

            _mediator = mediator;
            
            _eventStore = eventStore;
            
            _shifts = shifts;
        }

        public async Task Handle(ShiftCreatedEvent notification, CancellationToken cancellationToken)
        {
            var shiftToAdd = _mapper.Map<ShiftReader>(notification.ShiftWriter);

            var result = await _shifts.AddAsync(shiftToAdd, cancellationToken);

            await RaiseShiftAddedEvent(shiftToAdd, cancellationToken);
        }

        private async Task RaiseShiftAddedEvent(ShiftReader shiftAdded, CancellationToken cancellationToken)
        {
            var e = new ShiftAddedEvent(shiftAdded);

            var previousEvents = await _eventStore.GetAllByAggregateId(e.AggregateId, cancellationToken).ToListAsync(cancellationToken);

            await _eventStore.SaveAsync(e.Id, e.MajorVersion, previousEvents.AsReadOnly(), e.Name, cancellationToken);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
