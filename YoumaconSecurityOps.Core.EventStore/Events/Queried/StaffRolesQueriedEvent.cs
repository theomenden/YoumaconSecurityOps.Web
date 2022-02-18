namespace YoumaconSecurityOps.Core.EventStore.Events.Queried;

public class StaffRolesQueriedEvent : EventBase
{
    public StaffRolesQueriedEvent(StaffRolesQueryStringParameters? queryParameters)
    {
        QueryParameters = queryParameters;

        DataAsJson = queryParameters?.ToJson() ?? "No parameters used";
    }

    public StaffRolesQueryStringParameters? QueryParameters { get; }
}