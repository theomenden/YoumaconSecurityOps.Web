using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoumaconSecurityOps.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<LocationController> _logger;

        public LocationController(IMediator mediator, ILogger<LocationController> logger)
        {
            _mediator = mediator;

            _logger = logger;
        }

        // GET: api/<LocationController>
        [HttpGet(nameof(GetLocations))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IAsyncEnumerable<LocationReader>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IAsyncEnumerable<LocationReader>>> GetLocations([FromQuery]GetLocationsQuery query, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{GetLocations}([FromQuery]GetLocationsQuery query) \n GetLocationsQuery:{@query}", nameof(GetLocations), query);

            var locations = await _mediator.Send(query, cancellationToken);

            if (locations?.GetAsyncEnumerator(cancellationToken).Current is null)
            {
                return NoContent();
            }

            return Ok(locations);
        }

        // GET api/<LocationController>/5
        [HttpGet(nameof(GetHotels))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IAsyncEnumerable<LocationReader>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IAsyncEnumerable<LocationReader>>> GetHotels([FromQuery]GetLocationsWithParametersQuery parameters, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{GetHotels}GetHotels([FromQuery]GetLocationsWithParametersQuery parameters) \n GetLocationsWithParametersQuery:{@parameters}", nameof(GetHotels), parameters);
            var hotels = await _mediator.Send(parameters, cancellationToken);

            if (hotels?.GetAsyncEnumerator(cancellationToken).Current is null)
            {
                return NoContent();
            }

            return Ok(hotels);
        }

        // POST api/<LocationController>
        [HttpPost(nameof(AddLocation))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AddLocation([FromBody]AddLocationCommand command, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(command, cancellationToken);

            return Created(Request.Path.Value,null);
        }
    }
}
