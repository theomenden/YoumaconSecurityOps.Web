namespace YoumaconSecurityOps.Core.EventStore.Events.Created;

/// <summary>
/// Container for a created <see cref="StaffTypeRoleMapWriter"/>
/// <inheritdoc cref="EventBase"/>
/// </summary>
public class StaffTypeRoleMapCreatedEvent : EventBase
{
    public StaffTypeRoleMapCreatedEvent(StaffTypeRoleMapWriter staffTypeRoleMapWriter)
    {
        StaffTypeRoleMapWriter = staffTypeRoleMapWriter;
        DataAsJson = staffTypeRoleMapWriter.ToJson();
        Aggregate = $"{staffTypeRoleMapWriter.Id}-{staffTypeRoleMapWriter.StaffTypeId}-{staffTypeRoleMapWriter.RoleId}";
        AggregateId = staffTypeRoleMapWriter.Id;
        MajorVersion = 1;
        MinorVersion = 1;
    }

    public StaffTypeRoleMapWriter StaffTypeRoleMapWriter { get; }
}

