using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Models.Writers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Created
{
    public class LocationCreatedEvent : EventBase
    {
        public LocationCreatedEvent(LocationWriter locationAdded)
        {
            LocationAdded = locationAdded;
        }

        public LocationWriter LocationAdded { get; }
    }
}
