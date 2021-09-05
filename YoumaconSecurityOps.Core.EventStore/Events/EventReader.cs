using System;
using YoumaconSecurityOps.Core.Shared.Models;

namespace YoumaconSecurityOps.Core.EventStore.Events
{
    /// <summary>
    /// <para>Class intended to be used to read events from the database</para>
    /// <inheritdoc cref="IEntity"/>
    /// <inheritdoc cref="IEquatable{T}"/>
    /// <inheritdoc cref="IComparable{T}"/>
    /// </summary>
    public class EventReader : IEntity, IEquatable<EventReader>, IComparable<EventReader>
    {
        /// <value>
        /// The ID of the event, Primary Key
        /// </value>
        public Guid Id { get; set; }

        /// <value>
        /// The Date and time the event was created
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <value>
        /// The Major version of the event
        /// </value>
        public Int32 MajorVersion { get; set; }

        /// <value>
        /// The minor version of the event
        /// </value>
        public Int32 MinorVersion { get; set; }

        /// <value>
        /// The aggregate that the event takes place under
        /// </value>
        public String Aggregate { get; set; }

        /// <value>
        /// The data that event carries through processing
        /// </value>
        public String Data { get; set; }

        /// <value>
        /// The name of the event
        /// </value>
        public String Name { get; set; }

        #region Implementations & Overrides
        public bool Equals(EventReader other)
        {
            if (other is null)
            {
                return false;
            }

            return Id.Equals(other.Id) &&
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

            return MajorVersion == other.MajorVersion ? MinorVersion.CompareTo(other.MinorVersion) : MajorVersion.CompareTo(other.MajorVersion);
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
            return 53 * HashCode.Combine(Id, CreatedAt);
        }

        public static bool operator ==(EventReader lhs, EventReader rhs)
        {
            return lhs?.Equals(rhs) ?? rhs is null;
        }

        public static bool operator !=(EventReader lhs, EventReader rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
