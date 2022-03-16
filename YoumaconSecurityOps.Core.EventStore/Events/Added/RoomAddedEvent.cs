namespace YoumaconSecurityOps.Core.EventStore.Events.Added;

public sealed class RoomAddedEvent : EventBase
{
    public RoomAddedEvent(RoomScheduleReader roomScheduleReader)
    {
        RoomScheduleReader = roomScheduleReader ?? throw new ArgumentNullException(nameof(roomScheduleReader));
    }

    public RoomScheduleReader RoomScheduleReader { get; }
}