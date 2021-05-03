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
    internal sealed class GetStaffWithParametersQueryHandler: IRequestHandler<GetStaffWithParametersQuery, IAsyncEnumerable<StaffReader>>
    {
        private readonly IStaffAccessor _staff;

        private readonly IMediator _mediator;

        private readonly ILogger<GetStaffWithParametersQueryHandler> _logger;

        public GetStaffWithParametersQueryHandler(IStaffAccessor staff, IMediator mediator, ILogger<GetStaffWithParametersQueryHandler> logger)
        {
            _staff = staff;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IAsyncEnumerable<StaffReader>> Handle(GetStaffWithParametersQuery request, CancellationToken cancellationToken)
        {
            await RaiseStaffListQueriedEvent(request.Parameters, cancellationToken);

            var staff = Filter(request.Parameters,_staff.GetAll(cancellationToken));

            return staff;
        }

        private IAsyncEnumerable<StaffReader> Filter(StaffQueryStringParameters parameters, IAsyncEnumerable<StaffReader> staffList)
        {
            return staffList
                .Where(s => s.IsBlackShirt == parameters.IsBlackShirt)
                .Where(s => s.IsRaveApproved == parameters.IsRaveApproved)
                .Where(s => s.NeedsCrashSpace == parameters.NeedsCrashSpace)
                .Where(s => s.IsOnBreak == parameters.IsOnBreak)
                .Where(s => s.RoleId == parameters.RoleId)
                .Where(s => s.StaffTypeId == parameters.TypeId);

        }

        private async Task RaiseStaffListQueriedEvent(StaffQueryStringParameters parameters,
            CancellationToken cancellationToken)
        {
            var e = new StaffListQueriedEvent(parameters);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
