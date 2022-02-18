namespace YoumaconSecurityOps.Core.EventStore.Events.Queried;

public class IncidentListQueriedEvent : EventBase
{
    public IncidentListQueriedEvent(IncidentQueryStringParameters? queryParameters)
    {

        QueryParameters = queryParameters;

        DataAsJson = queryParameters?.ToJson();
    }

    public IncidentQueryStringParameters QueryParameters { get; }
}