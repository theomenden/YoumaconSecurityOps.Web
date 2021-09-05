using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using YoumaconSecurityOps.Core.EventStore.Events.Failed;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.FailureHandlers
{
    internal sealed class FailedToAddEntityEventHandler : INotificationHandler<FailedToAddEntityEvent>
    {
        public Task Handle(FailedToAddEntityEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
