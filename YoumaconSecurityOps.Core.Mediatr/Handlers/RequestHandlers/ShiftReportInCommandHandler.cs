using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Updated;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class ShiftReportInCommandHandler: AsyncRequestHandler<ShiftReportInCommand>
    {
        private readonly IMediator _mediator;

        private readonly IShiftRepository _shifts;

        private readonly ILogger<ShiftReportInCommandHandler> _logger;

        public ShiftReportInCommandHandler(IMediator mediator, ILogger<ShiftReportInCommandHandler> logger)
        {
            _mediator = mediator;

            _logger = logger;
        }

        protected override async Task Handle(ShiftReportInCommand request, CancellationToken cancellationToken)
        {
           var updatedShift = await _shifts.ReportIn(request.Id, request.ReportInAt, request.CurrentLocation, cancellationToken);

           await RaiseShiftUpdatedEvent(updatedShift, cancellationToken);
        }

        private async Task RaiseShiftUpdatedEvent(ShiftReader updatedShift, CancellationToken cancellationToken)
        {
            var e = new ShiftUpdatedEvent(updatedShift);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
