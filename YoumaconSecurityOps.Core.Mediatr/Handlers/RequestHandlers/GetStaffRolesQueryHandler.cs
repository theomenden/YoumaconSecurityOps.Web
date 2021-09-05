using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events;
using YoumaconSecurityOps.Core.EventStore.Events.Queried;
using YoumaconSecurityOps.Core.EventStore.Storage;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class GetStaffRolesQueryHandler : IRequestHandler<GetStaffRolesQuery, IAsyncEnumerable<StaffRole>>
    {
        private readonly IStaffRoleAccessor _staffRoles;

        private readonly IEventStoreRepository _eventStore;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        private readonly ILogger<GetStaffQueryHandler> _logger;

        public GetStaffRolesQueryHandler(IStaffRoleAccessor staffRoles, IMediator mediator, ILogger<GetStaffQueryHandler> logger, IEventStoreRepository eventStore, IMapper mapper)
        {
            _staffRoles = staffRoles;
            _mediator = mediator;
            _logger = logger;
            _eventStore = eventStore;
            _mapper = mapper;
        }

        public async Task<IAsyncEnumerable<StaffRole>> Handle(GetStaffRolesQuery request, CancellationToken cancellationToken = default)
        {
            var staff = _staffRoles.GetAll(cancellationToken);

            await RaiseStaffRolesQueriedEvent(request, cancellationToken);

            return staff;
        }
        private async Task RaiseStaffRolesQueriedEvent(GetStaffRolesQuery staffRoleQueryRequest, CancellationToken cancellationToken)
        {
            var e = new StaffRolesQueriedEvent(null)
            {
                Aggregate = nameof(GetStaffRolesQuery),
                MajorVersion = 1,
                Name = nameof(staffRoleQueryRequest)
            };

            _logger.LogInformation("Logged event of type {StaffRolesQueriedEvent} {e}, {request}, {StaffRolesQueriedEvent}", typeof(StaffRolesQueriedEvent), e, staffRoleQueryRequest, nameof(RaiseStaffRolesQueriedEvent));

            await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellationToken);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
