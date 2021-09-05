using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Added
{
    public class IncidentAddedEvent : EventBase
    {
        public IncidentAddedEvent(IncidentReader incident)
        {
            Incident = incident ?? throw new ArgumentNullException(nameof(incident));
        }

        public IncidentReader Incident { get; }
    }
}
