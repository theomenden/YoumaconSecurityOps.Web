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
    internal sealed class GetStaffTypesQueryHandler : IRequestHandler<GetStaffTypesQuery, IAsyncEnumerable<StaffType>>
    {
        private readonly IStaffTypeAccessor _staffTypes;

        private readonly IEventStoreRepository _eventStore;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        private readonly ILogger<GetStaffTypesQueryHandler> _logger;

        public GetStaffTypesQueryHandler(IStaffTypeAccessor staffTypes, IMediator mediator, ILogger<GetStaffTypesQueryHandler> logger, IEventStoreRepository eventStore, IMapper mapper)
        {
            _staffTypes = staffTypes;
            _mediator = mediator;
            _logger = logger;
            _eventStore = eventStore;
            _mapper = mapper;
        }

        public async Task<IAsyncEnumerable<StaffType>> Handle(GetStaffTypesQuery request, CancellationToken cancellationToken = default)
        {
            var staff = _staffTypes.GetAll(cancellationToken);

            await RaiseStaffTypesQueriedEvent(request, cancellationToken);

            return staff;
        }
        private async Task RaiseStaffTypesQueriedEvent(GetStaffTypesQuery staffRoleQueryRequest, CancellationToken cancellationToken)
        {
            var e = new StaffTypesQueriedEvent(null)
            {
                Aggregate = nameof(GetStaffTypesQuery),
                MajorVersion = 1,
                Name = nameof(staffRoleQueryRequest)
            };

            _logger.LogInformation("Logged event of type {StaffTypesQueriedEvent} {e}, {request}, {RaiseStaffTypesQueriedEvent}", typeof(StaffTypesQueriedEvent), e, staffRoleQueryRequest, nameof(RaiseStaffTypesQueriedEvent));

            await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellationToken);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
