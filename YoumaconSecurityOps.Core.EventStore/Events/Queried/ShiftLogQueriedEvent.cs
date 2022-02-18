namespace YoumaconSecurityOps.Core.EventStore.Events.Queried;

public class ShiftLogQueriedEvent: EventBase
{
    public ShiftLogQueriedEvent(ShiftQueryStringParameters? queryStringParameters)
    {
        QueryParameters = queryStringParameters;

        DataAsJson = queryStringParameters?.ToJson();
    }

    private ShiftQueryStringParameters? QueryParameters { get; }
}