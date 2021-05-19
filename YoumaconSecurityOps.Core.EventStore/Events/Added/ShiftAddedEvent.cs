using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Added
{
    public class ShiftAddedEvent: EventBase
    {
        public ShiftAddedEvent(ShiftReader shift)
        {
            Shift = shift ?? throw new ArgumentNullException(nameof(shift));
        }

        public ShiftReader Shift { get; }
    }
}
