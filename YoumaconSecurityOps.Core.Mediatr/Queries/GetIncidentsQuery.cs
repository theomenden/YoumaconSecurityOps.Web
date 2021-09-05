using System.Collections.Generic;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    public class GetIncidentsQuery: QueryBase<IAsyncEnumerable<IncidentReader>>
    {
    }
}
