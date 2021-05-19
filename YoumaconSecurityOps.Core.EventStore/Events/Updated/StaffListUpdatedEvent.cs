using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Updated
{
    public class StaffListUpdatedEvent : EventBase
    {
        public StaffListUpdatedEvent(StaffReader staffReader)
        {
            StaffReader = staffReader;
        }

        public StaffReader StaffReader { get; }
    }
}
