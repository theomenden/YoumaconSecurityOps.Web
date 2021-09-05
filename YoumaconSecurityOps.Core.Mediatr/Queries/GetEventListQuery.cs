using System.Collections.Generic;
using YoumaconSecurityOps.Core.EventStore.Events;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    public class GetEventListQuery: QueryBase<IAsyncEnumerable<EventReader>>
    {
    }
}
