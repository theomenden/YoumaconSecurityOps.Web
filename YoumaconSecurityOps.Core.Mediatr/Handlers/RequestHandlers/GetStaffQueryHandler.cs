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
    internal sealed class GetStaffQueryHandler: IRequestHandler<GetStaffQuery, IAsyncEnumerable<StaffReader>>
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

        public async Task<IAsyncEnumerable<StaffReader>> Handle(GetStaffQuery request, CancellationToken cancellationToken)
        {
            var staff = _staff.GetAll(cancellationToken);

            await RaiseStaffListQueriedEvent(cancellationToken);

            return staff;
        }

        private async Task RaiseStaffListQueriedEvent(CancellationToken cancellationToken)
        {
            var e = new StaffListQueriedEvent();

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
