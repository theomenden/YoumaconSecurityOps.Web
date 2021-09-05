using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Failed;
using YoumaconSecurityOps.Core.EventStore.Events.Updated;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class ReturnFromBreakCommandHandler : IRequestHandler<ReturnFromBreakCommand, Guid>
    {
        private readonly IMediator _mediator;

        private readonly IStaffRepository _staff;

        private readonly ILogger<ReturnFromBreakCommandHandler> _logger;

        public ReturnFromBreakCommandHandler(IMediator mediator, IStaffRepository staff, ILogger<ReturnFromBreakCommandHandler> logger)
        {
            _mediator = mediator;
            _staff = staff;
            _logger = logger;
        }

        public async Task<Guid> Handle(ReturnFromBreakCommand request, CancellationToken cancellationToken)
        {
            var couldUpdateStaffMember = await _staff.ReturnFromBreak(request.StaffId, cancellationToken);

            if (couldUpdateStaffMember is null)
            {
                await RaiseFailedToUpdateEntityEvent(request, cancellationToken);

                return Guid.Empty;
            }

            await RaiseStaffMemberUpdatedEvent(couldUpdateStaffMember, cancellationToken);

            return request.StaffId;
        }

        private async Task RaiseStaffMemberUpdatedEvent(StaffReader updatedStaff, CancellationToken cancellationToken)
        {
            var e = new StaffMemberUpdatedEvent(updatedStaff);

            await _mediator.Publish(e, cancellationToken);
        }

        private Task RaiseFailedToUpdateEntityEvent(ReturnFromBreakCommand command,
            CancellationToken cancellationToken)
        {
            var e = new FailedToUpdateEntityEvent();

            _mediator.Publish(e, cancellationToken);

            return Task.CompletedTask;
        }
    }
}
