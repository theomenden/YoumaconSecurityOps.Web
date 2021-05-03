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
    internal sealed class GetStaffQueryHandler: RequestHandler<GetStaffQuery, IAsyncEnumerable<StaffReader>>
    {
        private readonly IStaffAccessor _staff;

        private readonly IMediator _mediator;

        private readonly ILogger<GetStaffQueryHandler> _logger;

        public GetStaffQueryHandler(IStaffAccessor staff, IMediator mediator, ILogger<GetStaffQueryHandler> logger)
        {
            _staff = staff;
            _mediator = mediator;
            _logger = logger;
        }

        protected override IAsyncEnumerable<StaffReader> Handle(GetStaffQuery request)
        {
            var staff = _staff.GetAll();

            RaiseStaffListQueriedEvent(request);

            return staff;
        }

        private void RaiseStaffListQueriedEvent(GetStaffQuery query)
        {
            var e = new StaffListQueriedEvent
            {
                AggregateId = query.Id.ToString(),
                Aggregate = nameof(GetStaffQuery),
                MajorVersion = 1,
                MinorVersion = 1,
                Name = nameof(StaffListQueriedEvent)
            };
            _mediator.Publish(e);
        }
    }
}
