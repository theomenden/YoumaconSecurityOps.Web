using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.EventStore.Events
{
    public abstract record EventRecordBase(Guid AggregateId) : IEvent
    {
        public Guid Id => Guid.NewGuid();
    }
}
