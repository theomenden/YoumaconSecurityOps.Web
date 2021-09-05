using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Queried;
using YoumaconSecurityOps.Core.EventStore.Storage;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class GetStaffWithParametersQueryHandler: RequestHandler<GetStaffWithParametersQuery, IAsyncEnumerable<StaffReader>>
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
        

        protected override IAsyncEnumerable<StaffReader> Handle(GetStaffWithParametersQuery request)
        {
            RaiseStaffListQueriedEvent(request.Parameters);

            var staff = Filter(request.Parameters, _staff.GetAll());

            return staff;
        }

        private static IAsyncEnumerable<StaffReader> Filter(StaffQueryStringParameters parameters, IAsyncEnumerable<StaffReader> staffList)
        {
            return staffList
                .Where(s => s.IsBlackShirt == parameters.IsBlackShirt)
                .Where(s => s.IsRaveApproved == parameters.IsRaveApproved)
                .Where(s => s.NeedsCrashSpace == parameters.NeedsCrashSpace)
                .Where(s => s.IsOnBreak == parameters.IsOnBreak)
                .Where(s => s.Role?.Id == parameters.RoleId)
                .Where(s => s.StaffType?.Id == parameters.TypeId);
        }

        private void RaiseStaffListQueriedEvent(StaffQueryStringParameters parameters)
        {
            var e = new StaffListQueriedEvent(parameters)
            {
                Aggregate = nameof(StaffQueryStringParameters),
                MajorVersion = 1,
                MinorVersion = 1,
                Name = nameof(StaffListQueriedEvent)
            };

            _mediator.Publish(e);
        }
    }
}
