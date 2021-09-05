using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.EventStore.Events.Queried
{
    public class StaffRolesQueriedEvent : EventBase
    {
        public StaffRolesQueriedEvent(StaffRolesQueryStringParameters? queryParameters)
        {
            QueryParameters = queryParameters;

            DataAsJson = queryParameters?.ToJson() ?? "No parameters used";
        }

        public StaffRolesQueryStringParameters? QueryParameters { get; }
    }

}
