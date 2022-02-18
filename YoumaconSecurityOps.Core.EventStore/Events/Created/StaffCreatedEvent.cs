namespace YoumaconSecurityOps.Core.EventStore.Events.Created;

public class StaffCreatedEvent : EventBase
{
    public StaffCreatedEvent(StaffWriter staffWriter)
    {
        StaffWriter = staffWriter;
    }

    public StaffWriter StaffWriter { get; }
}