using System;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Updated
{
    public class StaffMemberUpdatedEvent : EventBase
    {
        public StaffMemberUpdatedEvent(StaffReader updatedStaff)
        {
            UpdatedStaff = updatedStaff ?? throw new ArgumentNullException(nameof(updatedStaff));
        }

        public StaffReader UpdatedStaff { get; }
    }
}
