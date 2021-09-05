using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.EventStore.Events.Queried
{
    public class ShiftLogQueriedEvent: EventBase
    {
        public ShiftLogQueriedEvent(ShiftQueryStringParameters? queryStringParameters)
        {
            QueryParameters = queryStringParameters;

            DataAsJson = queryStringParameters?.ToJson();
        }

        private ShiftQueryStringParameters? QueryParameters { get; }
    }
}
