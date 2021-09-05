using System;

namespace YoumaconSecurityOps.Core.EventStore.Events.Failed
{
    /// <summary>
    /// An event that represents the failure for the system to add a given entity
    /// </summary>
    public class FailedToAddEntityEvent: EventBase
    {
        public FailedToAddEntityEvent(Guid aggregateId, Type aggregateType)
        {
            AggregateId = aggregateId;
            DataAsJson = $"Failed to Add {aggregateType} to database";
        }
    }
}
