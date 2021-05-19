using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Updated
{
    public class ShiftLogUpdatedEvent: EventBase
    {
        public ShiftLogUpdatedEvent(ShiftReader shiftReader)
        {
            ShiftReader = shiftReader;
        }

        public ShiftReader ShiftReader { get; }
    }
}
