namespace YoumaconSecurityOps.Core.EventStore.Events.Updated;

public class StaffListUpdatedEvent : EventBase
{
    public StaffListUpdatedEvent(StaffReader staffReader)
    {
        StaffReader = staffReader;
    }

    public StaffReader StaffReader { get; }
}