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
    internal sealed class GetShiftListQueryHandler: RequestHandler<GetShiftListQuery, IAsyncEnumerable<ShiftReader>>
    {
        private readonly ILogger<GetShiftListQueryHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IShiftAccessor _shifts;

        public GetShiftListQueryHandler(ILogger<GetShiftListQueryHandler> logger, IMediator mediator, IShiftAccessor shifts)
        {
            _logger = logger;
            _mediator = mediator;
            _shifts = shifts;
        }
        
        protected override IAsyncEnumerable<ShiftReader> Handle(GetShiftListQuery request)
        {
            var shiftLog = _shifts.GetAll();

            RaiseShiftLogQueriedEvent(request);

            return shiftLog;
        }

        private void RaiseShiftLogQueriedEvent(GetShiftListQuery request)
        {
            var e = new ShiftLogQueriedEvent(null)
            {
                Aggregate = nameof(GetShiftListQuery),
                MajorVersion = 1,
                Name = nameof(ShiftLogQueriedEvent)
            };

            _mediator.Publish(e);
        }
    }
}
