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

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class GetIncidentsQueryHandler: RequestHandler<GetIncidentsQuery, IAsyncEnumerable<IncidentReader>>
    {
        private readonly IIncidentAccessor _incidents;

        private readonly IMediator _mediator;

        private readonly ILogger<GetIncidentsQueryHandler> _logger;

        public GetIncidentsQueryHandler(IIncidentAccessor incidents, IMediator mediator, ILogger<GetIncidentsQueryHandler> logger)
        {
            _incidents = incidents;
            _mediator = mediator;
            _logger = logger;   
        }

        protected override IAsyncEnumerable<IncidentReader> Handle(GetIncidentsQuery request)
        {
            var staff = _incidents.GetAll();

            RaiseIncidentListQueriedEvent(request);

            return staff;
        }

        private void RaiseIncidentListQueriedEvent(GetIncidentsQuery request)
        {
            var e = new IncidentListQueriedEvent(null)
            {
                Aggregate = nameof(GetIncidentsQuery),
                MajorVersion = 1,
                Name = nameof(IncidentListQueriedEvent)
            };

            _mediator.Publish(e);
        }
    }
}
