using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace YoumaconSecurityOps.Core.EventStore.Events
{
    public interface IEvent: INotification
    {
        Guid Id { get; }
    }
}
