using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Added
{
    public class LocationAddedEvent : EventBase
    {
        public LocationAddedEvent(LocationReader locationAdded)
        {
            LocationAdded = locationAdded;

            DataAsJson = locationAdded.ToJson();
        }

        public LocationReader LocationAdded { get; }
    }
}
