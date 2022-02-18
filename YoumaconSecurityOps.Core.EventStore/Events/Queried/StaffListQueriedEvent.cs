#nullable enable
namespace YoumaconSecurityOps.Core.EventStore.Events.Queried;

public class StaffListQueriedEvent : EventBase
{
    public StaffListQueriedEvent(StaffQueryStringParameters? queryParameters)
    {
        QueryParameters = queryParameters;

        DataAsJson = queryParameters?.ToJson();
    }

    public StaffQueryStringParameters? QueryParameters { get; }
}