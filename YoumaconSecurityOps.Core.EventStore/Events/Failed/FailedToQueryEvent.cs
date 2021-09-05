using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.EventStore.Events.Failed
{
    /// <summary>
    /// An event that represents the failure of the system to return the results of a query to the user
    /// </summary>
    public class FailedToQueryEvent: EventBase
    {
        public FailedToQueryEvent()
        {
            
        }
    }
}
