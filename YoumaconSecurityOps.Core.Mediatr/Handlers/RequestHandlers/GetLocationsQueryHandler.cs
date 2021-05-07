using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Queried;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, IAsyncEnumerable<LocationReader>>
    {
        private readonly ILocationAccessor _locations;

        private readonly IMediator _mediator;

        private readonly ILogger<GetLocationsQueryHandler> _logger;

        public GetLocationsQueryHandler(ILocationAccessor locations, IMediator mediator, ILogger<GetLocationsQueryHandler> logger)
        {
            _locations = locations;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IAsyncEnumerable<LocationReader>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            await RaiseLocationsQueriedEvent(request,cancellationToken);

            return _locations.GetAll(cancellationToken);
        }

        private async Task RaiseLocationsQueriedEvent(GetLocationsQuery request,CancellationToken cancellationToken)
        {

            var e = new LocationListQueriedEvent
            {
                Aggregate = nameof(GetLocationsQuery),
                DataAsJson = String.Empty,
                AggregateId = request.Id.ToString(),
                MajorVersion = 1,
                MinorVersion = 1,
                Name = nameof(request)
            };

            _logger.LogInformation("Logged event of type {LocationListQueriedEvent} {e}, {request}, {RaiseLocationsQueriedEvent}", typeof(LocationListQueriedEvent), e, request, nameof(RaiseLocationsQueriedEvent));

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
