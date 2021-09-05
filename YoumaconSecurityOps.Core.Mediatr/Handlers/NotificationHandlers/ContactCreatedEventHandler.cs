using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events.Added;
using YoumaconSecurityOps.Core.EventStore.Events.Created;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class ContactCreatedEventHandler : INotificationHandler<ContactCreatedEvent>
    {
        private readonly IContactRepository _contacts;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        private readonly ILogger<ContactCreatedEventHandler> _logger;

        public ContactCreatedEventHandler(IContactRepository contacts, IMapper mapper, IMediator mediator, ILogger<ContactCreatedEventHandler> logger)
        {
            _contacts = contacts;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(ContactCreatedEvent notification, CancellationToken cancellationToken)
        {
            var contactToAdd = _mapper.Map<ContactReader>(notification.ContactWriter);

            await _contacts.AddAsync(contactToAdd, cancellationToken);

            await RaiseContactAddedEvent(contactToAdd, cancellationToken);
        }

        private async Task RaiseContactAddedEvent(ContactReader contactReader, CancellationToken cancellationToken)
        {
            var e = new ContactAddedEvent(contactReader);

            await _mediator.Publish(e, cancellationToken);
        }
    }
}
