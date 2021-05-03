using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Models.Writers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Created
{
    public record LocationCreatedEvent(LocationWriter LocationAdded) : EventBase(LocationAdded.ToJson());
}
