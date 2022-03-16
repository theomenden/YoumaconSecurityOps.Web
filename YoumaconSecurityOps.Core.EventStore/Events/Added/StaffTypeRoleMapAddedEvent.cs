﻿namespace YoumaconSecurityOps.Core.EventStore.Events.Added;

public class StaffTypeRoleMapAddedEvent : EventBase
{
    public StaffTypeRoleMapAddedEvent(StaffTypesRoles staffTypeRoleMap)
    {
        StaffTypeRoleMap = staffTypeRoleMap;
        DataAsJson = JsonSerializer.Serialize(staffTypeRoleMap, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        Aggregate = $"{staffTypeRoleMap.Id}-{staffTypeRoleMap.StaffTypeId}-{staffTypeRoleMap.RoleId}";
        AggregateId = staffTypeRoleMap.Id;
    }

    public StaffTypesRoles StaffTypeRoleMap { get; }
}

