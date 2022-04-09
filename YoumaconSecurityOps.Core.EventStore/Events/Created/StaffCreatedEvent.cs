namespace YoumaconSecurityOps.Core.EventStore.Events.Created;

public class StaffCreatedEvent : EventBase
{
    public StaffCreatedEvent(FullStaffWriter fullStaffWriter)
    {
        StaffWriter = fullStaffWriter;
        DataAsJson = fullStaffWriter.ToJson();
        Aggregate = $"{fullStaffWriter.Id}-{fullStaffWriter.StaffWriter.StaffTypeId}-{fullStaffWriter.StaffWriter.RoleId}";
        AggregateId = fullStaffWriter.Id;
        MajorVersion = 1;
        MinorVersion = 1;
    }

    public FullStaffWriter StaffWriter { get; }
}