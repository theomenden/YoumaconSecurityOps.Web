namespace YoumaconSecurityOps.Core.EventStore.Events.Added;

public class LocationAddedEvent : EventBase
{
    public LocationAddedEvent(LocationReader locationAdded)
    {
        LocationAdded = locationAdded;

        DataAsJson = locationAdded.ToJson();
    }

    public LocationReader LocationAdded { get; }
}