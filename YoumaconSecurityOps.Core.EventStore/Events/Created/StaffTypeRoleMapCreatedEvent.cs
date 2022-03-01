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
    }

    public StaffTypeRoleMapWriter StaffTypeRoleMapWriter { get; }
}

