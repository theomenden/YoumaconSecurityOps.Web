using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.EventStore.Events
{

    public abstract record EventBase(string DataAsJson): IEvent
    {
        public Guid Id => Guid.NewGuid();

        public DateTime CreatedAt => DateTime.Now;

        public  Int32 MajorVersion { get; set; }

        public Int32 MinorVersion { get; set; }

        public String Aggregate { get; set; }

        public String AggregateId { get; set; }

        public String Name { get; set; }
    }
}
