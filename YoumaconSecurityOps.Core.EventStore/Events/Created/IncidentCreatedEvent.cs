using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Models.Writers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Created
{
    public class IncidentCreatedEvent: EventBase
    {
        public IncidentCreatedEvent(IncidentWriter incidentWriter)
        {
            IncidentWriter = incidentWriter;
            DataAsJson = incidentWriter.ToJson();
            Aggregate = $"{Id}-{incidentWriter.Title}";
            AggregateId = incidentWriter.Id;
            MajorVersion = 1;
            MinorVersion = 1;
        }

        /// <value>
        /// A prototype shift object
        /// </value>
        public IncidentWriter IncidentWriter { get; }
    }
}
