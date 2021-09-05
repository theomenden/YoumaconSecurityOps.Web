using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Models.Writers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Created
{
    /// <summary>
    /// An event that reflects the creation of a new shift
    /// </summary>
    /// <remarks>Holds an instance of the created <see cref="ShiftWriter"/></remarks>
    public class ShiftCreatedEvent: EventBase
    {
        public ShiftCreatedEvent(ShiftWriter shiftWriter)
        {
            ShiftWriter = shiftWriter;
            DataAsJson = shiftWriter.ToJson();
            Aggregate = $"{Id}-{shiftWriter.StaffMemberName}";
            AggregateId = shiftWriter.Id;
            MajorVersion = 1;
            MinorVersion = 1;
        }

        /// <value>
        /// A prototype shift object
        /// </value>
        public ShiftWriter ShiftWriter { get; }

    }
}
