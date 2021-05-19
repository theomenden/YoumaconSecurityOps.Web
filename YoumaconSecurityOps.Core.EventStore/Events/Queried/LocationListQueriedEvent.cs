#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.EventStore.Events.Queried
{
    public class LocationListQueriedEvent : EventBase
    {
        public LocationListQueriedEvent(LocationQueryStringParameters? parameters)
        {
            Parameters = parameters;

            DataAsJson = parameters.ToJson() ?? "No parameters used";
        }

        public LocationQueryStringParameters? Parameters { get; }
    }
}
