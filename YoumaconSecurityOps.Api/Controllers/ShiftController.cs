using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoumaconSecurityOps.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly ILogger<ShiftController> _logger;

        private readonly IMediator _mediator;

        public ShiftController(ILogger<ShiftController> logger, IMediator mediator)
        {
            _logger = logger;

            _mediator = mediator;
        }

        // GET: api/<ShiftController>
        [HttpGet(nameof(GetShifts))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IAsyncEnumerable<ShiftReader>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IAsyncEnumerable<ShiftReader>>> GetShifts([FromQuery] GetShiftListQuery query, CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }

        // GET api/<ShiftController>/params
        [HttpGet(nameof(GetShiftsWithParameters))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IAsyncEnumerable<ShiftReader>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IAsyncEnumerable<ShiftReader>>> GetShiftsWithParameters([FromQuery] GetShiftListWithParametersQuery parameters, CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(parameters, cancellationToken));
        }

        // POST api/<ShiftController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddShift([FromBody] AddShiftCommand command, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(command, cancellationToken);

            return Created(Request.Path.Value, null);
        }
    }
}
