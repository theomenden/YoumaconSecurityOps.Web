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

        public Task<IAsyncEnumerable<LocationReader>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            RaiseLocationsQueriedEvent(cancellationToken);

            return Task.FromResult(_locations.GetAll(cancellationToken));
        }

        private void RaiseLocationsQueriedEvent(CancellationToken cancellationToken)
        {

            var e = new LocationListQueriedEvent();

            _mediator.Publish(e, cancellationToken);
        }
    }
}
