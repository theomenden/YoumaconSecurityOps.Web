#nullable enable
namespace YoumaconSecurityOps.Core.EventStore.Events.Queried;

public class LocationListQueriedEvent : EventBase
{
    public LocationListQueriedEvent(LocationQueryStringParameters? parameters)
    {
        Parameters = parameters;

        DataAsJson = parameters.ToJson() ?? "No parameters used";
    }

    public LocationQueryStringParameters? Parameters { get; }
}