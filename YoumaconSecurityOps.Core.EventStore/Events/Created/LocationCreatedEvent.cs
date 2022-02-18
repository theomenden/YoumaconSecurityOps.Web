namespace YoumaconSecurityOps.Core.EventStore.Events.Created;

public class LocationCreatedEvent : EventBase
{
    public LocationCreatedEvent(LocationWriter locationAdded)
    {
        LocationAdded = locationAdded;
    }

    public LocationWriter LocationAdded { get; }
}