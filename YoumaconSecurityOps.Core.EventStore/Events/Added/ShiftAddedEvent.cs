namespace YoumaconSecurityOps.Core.EventStore.Events.Added;

public class ShiftAddedEvent : EventBase
{
    public ShiftAddedEvent(ShiftReader shift)
    {
        Shift = shift ?? throw new ArgumentNullException(nameof(shift));
    }

    public ShiftReader Shift { get; }
}