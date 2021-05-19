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
    public class ShiftCreatedEvent: EventBase
    {
        public ShiftCreatedEvent(ShiftWriter shiftWriter)
        {
            ShiftWriter = shiftWriter;
            DataAsJson = shiftWriter.ToJson();
            Aggregate = $"{Id}-{shiftWriter.StaffMember.ContactInformation.PreferredName}";
            AggregateId = shiftWriter.Id;
            MajorVersion = 1;
            MinorVersion = 1;
        }
        public ShiftWriter ShiftWriter { get; }

    }
}
