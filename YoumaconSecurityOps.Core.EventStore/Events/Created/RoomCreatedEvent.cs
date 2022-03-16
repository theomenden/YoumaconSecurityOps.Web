namespace YoumaconSecurityOps.Core.EventStore.Events.Created;

public class RoomCreatedEvent : EventBase
{
    public RoomCreatedEvent(RoomScheduleWriter roomScheduleWriter)
    {
        RoomScheduleWriter = roomScheduleWriter;
    }

    public RoomScheduleWriter RoomScheduleWriter { get; }
}