using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public class LocationService: ILocationService
    {
        private readonly IMediator _mediator;

        private readonly ILogger<LocationService> _logger;

        public LocationService(IMediator mediator, ILogger<LocationService> logger)
        {
            _mediator = mediator;
            _logger = logger;   
        }

        public async Task<List<LocationReader>> GetLocationsAsync(GetLocationsQuery locationsQuery, CancellationToken cancellationToken = default)
        {
            return await _mediator.CreateStream(locationsQuery, cancellationToken).ToListAsync(cancellationToken);
        }
    }
}
