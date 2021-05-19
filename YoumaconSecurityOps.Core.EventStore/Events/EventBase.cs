using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.EventStore.Events
{

    public abstract class EventBase: IEvent, IEquatable<EventBase>, IComparable<EventBase>
    {
        protected EventBase()
        {
            Id = Guid.NewGuid();

            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; }

        public Guid AggregateId { get; init; }

        public DateTime CreatedAt { get; }

        public Int32 MajorVersion { get; set; }

        public Int32 MinorVersion { get; set; }

        public String Aggregate { get; set; }
        
        public String Name { get; set; }

        public String DataAsJson { get; set; }

        public bool Equals(EventBase other)
        {
            if (other is null)
            {
                return false;
            }

            return Id == other.Id &&
                   MajorVersion == other.MajorVersion &&
                   MinorVersion == other.MinorVersion;
        }

        public int CompareTo(EventBase other)
        {
            if (other is null)
            {
                return 1;
            }

            if (Equals(other))
            {
                return 0;
            }

            if (MajorVersion == other.MajorVersion)
            {
                return MinorVersion.CompareTo(other.MinorVersion);
            }

            return MajorVersion.CompareTo(other.MajorVersion);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is EventBase other && Equals(other);
        }

        public override int GetHashCode()
        {
            return 53 * HashCode.Combine(Id, CreatedAt);
        }
    }
}
