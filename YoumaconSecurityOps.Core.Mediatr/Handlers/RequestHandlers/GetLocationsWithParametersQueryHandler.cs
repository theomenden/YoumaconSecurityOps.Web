using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Queried;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class GetLocationsWithParametersQueryHandler : IRequestHandler<GetLocationsWithParametersQuery, IAsyncEnumerable<LocationReader>>
    {
        private readonly ILocationAccessor _locations;

        private readonly IMediator _mediator;

        private readonly ILogger<GetLocationsWithParametersQueryHandler> _logger;

        public GetLocationsWithParametersQueryHandler(ILocationAccessor locations, IMediator mediator, ILogger<GetLocationsWithParametersQueryHandler> logger)
        {
            _locations = locations;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IAsyncEnumerable<LocationReader>> Handle(GetLocationsWithParametersQuery request, CancellationToken cancellationToken)
        {
            var locations =  _locations.GetAll(cancellationToken);
            
            await RaiseLocationListQueriedEvent(request.Parameters, cancellationToken);

            return Filter(locations, request.Parameters);
        }

        private static IAsyncEnumerable<LocationReader> Filter(IAsyncEnumerable<LocationReader> locations,
            LocationQueryStringParameters parameters)
        {
            if (!String.IsNullOrWhiteSpace(parameters.Name))
            {
                locations = locations.Where(l => l.Name.Equals(parameters.Name));
            }
            
            return locations.Where(l => l.IsHotel == parameters.IsHotel);
        }

        private async Task RaiseLocationListQueriedEvent(LocationQueryStringParameters parameters, CancellationToken cancellation)
        {
            var e = new LocationListQueriedEvent(parameters);

            await _mediator.Publish(e, cancellation);
        }
    }
}
