using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal class AddStaffTypeRoleMapCommandHandler: IRequestHandler<AddStaffTypeRoleMapCommand, Guid>
    {
        private readonly IDbContextFactory<EventStoreDbContext> _dbContextFactory;

        private readonly IEventStoreRepository _eventStore;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        public AddStaffTypeRoleMapCommandHandler(IDbContextFactory<EventStoreDbContext> dbContextFactory, IEventStoreRepository eventStore, IMapper mapper, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _eventStore = eventStore;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(AddStaffTypeRoleMapCommand request, CancellationToken cancellationToken)
        {
            await RaiseStaffTypeRoleMapCreatedEvent(request.StaffTypeRoleMapWriter, cancellationToken);

            return request.Id;
        }

        private async Task RaiseStaffTypeRoleMapCreatedEvent(StaffTypeRoleMapWriter typeRoleMapWriter, CancellationToken cancellationToken)
        {
            var e = new StaffTypeRoleMapCreatedEvent(typeRoleMapWriter)
            {
                Name= nameof(AddStaffTypeRoleMapCommand)
            };

            var mappedEvent = _mapper.Map<EventReader>(e);

            await using var context =
                await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

            await _eventStore.ApplyInitialEventAsync(context, mappedEvent, cancellationToken);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
