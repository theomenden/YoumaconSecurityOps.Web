using System;

namespace YoumaconSecurityOps.Core.EventStore.Events
{
    public abstract record EventRecordBase(Guid AggregateId) : IEvent
    {
        public Guid Id => Guid.NewGuid();
    }
}
