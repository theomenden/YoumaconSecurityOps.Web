using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YoumaconSecurityOps.Core.EventStore.Events
{
    public class EventReader : IEquatable<EventReader>, IComparable<EventReader>
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public Int32 MajorVersion { get; set; }

        public Int32 MinorVersion { get; set; }

        public String Aggregate { get; set; }

        public String AggregateId { get; set; }

        public String Data { get; set; }

        public String Name { get; set; }

        public bool Equals(EventReader other)
        {
            if (other is null)
            {
                return false;
            }

            return  AggregateId.Equals(other.AggregateId) &&
                    MajorVersion == other.MajorVersion &&
                    MinorVersion == other.MinorVersion;
        }

        public int CompareTo(EventReader other)
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

            return obj is EventReader other && Equals(other);
        }

        public override int GetHashCode()
        {
            return 53 * HashCode.Combine(Id, AggregateId, MinorVersion);
        }
    }
}
