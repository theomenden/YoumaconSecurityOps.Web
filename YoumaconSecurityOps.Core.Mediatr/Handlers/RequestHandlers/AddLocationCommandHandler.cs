using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Created;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Shared.Models.Writers;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class AddLocationCommandHandler: IRequestHandler<AddLocationCommand>
    {
        private readonly IMediator _mediator;

        private readonly ILogger<AddLocationCommandHandler> _logger;

        public AddLocationCommandHandler(IMediator mediator, ILogger<AddLocationCommandHandler> logger)
        {
            _mediator = mediator;

            _logger = logger;
        }

        public async Task<Unit> Handle(AddLocationCommand request, CancellationToken cancellationToken)
        {
            var locationRecord = new LocationWriter(request.Name, request.IsHotel);

            await RaiseLocationCreatedEvent(locationRecord, cancellationToken);

            return new Unit();
        }

        private async Task RaiseLocationCreatedEvent(LocationWriter locationWriter, CancellationToken cancellationToken)
        {
            var e = new LocationCreatedEvent(locationWriter)
            {
                Aggregate = nameof(LocationWriter),
                MajorVersion = 1,
                MinorVersion = 1,
                Name = locationWriter.Name
            };

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
