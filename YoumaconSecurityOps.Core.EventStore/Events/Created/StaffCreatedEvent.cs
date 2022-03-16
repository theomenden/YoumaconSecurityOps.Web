namespace YoumaconSecurityOps.Core.EventStore.Events.Created;

public class StaffCreatedEvent : EventBase
{
    public StaffCreatedEvent(StaffWriter staffWriter)
    {
        StaffWriter = staffWriter;
        DataAsJson = staffWriter.ToJson();
        Aggregate = $"{staffWriter.Id}-{staffWriter.StaffTypeId}-{staffWriter.RoleId}";
        AggregateId = staffWriter.Id;
        MajorVersion = 1;
        MinorVersion = 1;
    }

    public StaffWriter StaffWriter { get; }
}