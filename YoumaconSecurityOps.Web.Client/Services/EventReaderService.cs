using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using YoumaconSecurityOps.Core.EventStore.Events;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Enumerations;
using YoumaconSecurityOps.Web.Client.Models;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public class EventReaderService: IEventReaderService
    {
        private readonly IMediator _mediator;

        private readonly ILogger<EventReaderService> _logger;

        public EventReaderService(IMediator mediator, ILogger<EventReaderService> logger)
        {
            _mediator = mediator;
            _logger = logger;   
        }

        public async Task<List<EventReader>> GetAllEventsAsync(GetEventListQuery query, CancellationToken cancellationToken = default)
        {
            var eventStream = await _mediator.Send(query, cancellationToken);

            return await eventStream.ToListAsync(cancellationToken);
        }
    }
}
