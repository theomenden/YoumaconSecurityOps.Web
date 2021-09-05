using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    internal sealed class GetIncidentsWithParametersQueryHandler : RequestHandler<GetIncidentsWithParametersQuery, IAsyncEnumerable<IncidentReader>>
    {
        private readonly IIncidentAccessor _staff;

        private readonly IMediator _mediator;

        private readonly ILogger<GetIncidentsWithParametersQueryHandler> _logger;

        public GetIncidentsWithParametersQueryHandler(IIncidentAccessor staff, IMediator mediator, ILogger<GetIncidentsWithParametersQueryHandler> logger)
        {
            _staff = staff;
            _mediator = mediator;
            _logger = logger;
        }


        protected override IAsyncEnumerable<IncidentReader> Handle(GetIncidentsWithParametersQuery request)
        {
            RaiseStaffListQueriedEvent(request.Parameters);

            var filteredIncidents = Filter(request.Parameters, _staff.GetAll());

            return filteredIncidents;
        }

        private static IAsyncEnumerable<IncidentReader> Filter(IncidentQueryStringParameters parameters, IAsyncEnumerable<IncidentReader> incidents)
        {
            return incidents
                .Where(i => i.Title.Equals(parameters.Title))
                .Where(i => parameters.StaffIds.Contains(i.ReportedById))
                .Where(i => parameters.StaffIds.Contains(i.RecordedById))
                .Where(i => i.Severity == (int)parameters.Severity);
                
        }

        private void RaiseStaffListQueriedEvent(IncidentQueryStringParameters parameters)
        {
            var e = new IncidentListQueriedEvent(parameters)
            {
                Aggregate = nameof(IncidentQueryStringParameters),
                MajorVersion = 1,
                MinorVersion = 1,
                Name = nameof(IncidentListQueriedEvent)
            };

            _mediator.Publish(e);
        }
    }
}
