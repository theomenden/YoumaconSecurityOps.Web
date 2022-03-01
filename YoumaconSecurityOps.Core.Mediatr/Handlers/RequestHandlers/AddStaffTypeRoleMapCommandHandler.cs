using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal class AddStaffTypeRoleMapCommandHandler: IRequestHandler<AddStaffTypeRoleMapCommand, Guid>
    {
        private readonly ILogger<AddStaffTypeRoleMapCommandHandler> _logger;

        private readonly IMediator _mediator;

        public AddStaffTypeRoleMapCommandHandler(ILogger<AddStaffTypeRoleMapCommandHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(AddStaffTypeRoleMapCommand request, CancellationToken cancellationToken)
        {
            await RaiseStaffTypeRoleMapCreatedEvent(request.StaffTypeRoleMapWriter, cancellationToken);

            return request.Id;
        }

        private async Task RaiseStaffTypeRoleMapCreatedEvent(StaffTypeRoleMapWriter typeRoleMapWriter, CancellationToken cancellationToken)
        {
            var e = new StaffTypeRoleMapCreatedEvent(typeRoleMapWriter);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
