namespace YoumaconSecurityOps.Core.EventStore.Events.Queried;

public class RadioScheduleQueriedEvent: EventBase
{
    public RadioScheduleQueriedEvent(RadioScheduleQueryStringParameter? queryParameters)
    {

        QueryParameters = queryParameters;

        DataAsJson = queryParameters?.ToJson();
    }

    public RadioScheduleQueryStringParameter QueryParameters { get; }
}