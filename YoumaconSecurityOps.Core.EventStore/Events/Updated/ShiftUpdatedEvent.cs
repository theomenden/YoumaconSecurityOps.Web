using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Updated
{
    public class ShiftUpdatedEvent: EventBase
    {
        public ShiftUpdatedEvent(ShiftReader updatedShift)
        {
            UpdatedShift = updatedShift ?? throw new ArgumentNullException(nameof(updatedShift));
        }

        public ShiftReader UpdatedShift { get; }
    }
}
