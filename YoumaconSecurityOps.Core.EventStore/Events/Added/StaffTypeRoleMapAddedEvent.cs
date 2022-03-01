namespace YoumaconSecurityOps.Core.EventStore.Events.Added;

public class StaffTypeRoleMapAddedEvent : EventBase
{
    public StaffTypeRoleMapAddedEvent(StaffTypesRoles staffTypeRoleMap)
    {
        StaffTypeRoleMap = staffTypeRoleMap;
    }

    public StaffTypesRoles StaffTypeRoleMap { get; }
}

