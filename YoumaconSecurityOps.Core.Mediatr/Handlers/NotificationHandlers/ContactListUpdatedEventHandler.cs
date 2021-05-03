using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using YoumaconSecurityOps.Core.EventStore.Events.Updated;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers
{
    internal sealed class ContactListUpdatedEventHandler: INotificationHandler<ContactListUpdatedEvent>
    {
        public Task Handle(ContactListUpdatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
