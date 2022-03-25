namespace YoumaconSecurityOps.Core.EventStore.Events.Updated;

public class ShiftUpdatedEvent: EventBase
{
    public ShiftUpdatedEvent(ShiftReader updatedShift)
    {
        UpdatedShift = updatedShift;
    }

    public ShiftReader UpdatedShift { get; }
}