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

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class GetRadioScheduleQueryHandler : RequestHandler<GetRadioSchedule, IAsyncEnumerable<RadioScheduleReader>>
    {
        private readonly IRadioScheduleAccessor _radios;

        private readonly IMediator _mediator;

        private readonly ILogger<GetRadioScheduleQueryHandler> _logger;

        public GetRadioScheduleQueryHandler(IRadioScheduleAccessor radios, IMediator mediator, ILogger<GetRadioScheduleQueryHandler> logger)
        {
            _radios = radios;
            _mediator = mediator;
            _logger = logger;
        }

        protected override IAsyncEnumerable<RadioScheduleReader> Handle(GetRadioSchedule request)
        {
            var radios = _radios.GetAll();

            RaiseRadioScheduleQueriedEvent(request);

            return radios;
        }

        private void RaiseRadioScheduleQueriedEvent(GetRadioSchedule query)
        {
            var e = new RadioScheduleQueriedEvent(null)
            {
                Aggregate = nameof(GetRadioSchedule),
                MajorVersion = 1,
                Name = nameof(RadioScheduleQueriedEvent)
            };

            _mediator.Publish(e);
        }
    }
}
