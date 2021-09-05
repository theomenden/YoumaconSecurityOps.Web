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
    internal sealed class FailedToQueryEventHandler: INotificationHandler<FailedToQueryEvent>
    {
        public Task Handle(FailedToQueryEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
