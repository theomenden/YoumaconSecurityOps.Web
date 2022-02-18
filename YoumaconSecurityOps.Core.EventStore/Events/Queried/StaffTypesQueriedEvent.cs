namespace YoumaconSecurityOps.Core.EventStore.Events.Queried;

public class StaffTypesQueriedEvent : EventBase
{
    public StaffTypesQueriedEvent(StaffTypesQueryStringParameters? queryParameters)
    {
        QueryParameters = queryParameters;

        DataAsJson = queryParameters?.ToJson() ?? "No parameters used";
    }

    public StaffTypesQueryStringParameters? QueryParameters { get; }
}