using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events;
using YoumaconSecurityOps.Core.EventStore.Events.Queried;
using YoumaconSecurityOps.Core.EventStore.Storage;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, IAsyncEnumerable<LocationReader>>
    {
        private readonly IEventStoreRepository _eventStore;

        private readonly ILocationAccessor _locations;

        private readonly ILogger<GetLocationsQueryHandler> _logger;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        public GetLocationsQueryHandler(IEventStoreRepository eventStore, ILocationAccessor locations, ILogger<GetLocationsQueryHandler> logger, IMapper mapper, IMediator mediator)
        {
            _eventStore = eventStore;
            _logger = logger;
            _locations = locations;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IAsyncEnumerable<LocationReader>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            await RaiseLocationsQueriedEvent(request,cancellationToken);

            return _locations.GetAll(cancellationToken);
        }

        private async Task RaiseLocationsQueriedEvent(GetLocationsQuery locationQueryRequest,CancellationToken cancellationToken)
        {

            var e = new LocationListQueriedEvent(null)
            {
                Aggregate = nameof(GetLocationsQuery),
                MajorVersion = 1,
                Name = nameof(locationQueryRequest)
            };

            _logger.LogInformation("Logged event of type {LocationListQueriedEvent} {e}, {request}, {RaiseLocationsQueriedEvent}", typeof(LocationListQueriedEvent), e, locationQueryRequest, nameof(RaiseLocationsQueriedEvent));

            await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellationToken);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
