using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.EventStore.Events.Queried
{
    public class RadioScheduleQueriedEvent: EventBase
    {
        public RadioScheduleQueriedEvent(RadioScheduleQueryStringParameter? queryParameters)
        {

            QueryParameters = queryParameters;

            DataAsJson = queryParameters?.ToJson();
        }

        public RadioScheduleQueryStringParameter QueryParameters { get; }
    }
}
