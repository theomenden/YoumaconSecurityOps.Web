using System.Collections.Generic;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    public class GetShiftListQuery: QueryBase<IAsyncEnumerable<ShiftReader>>
    {
    }
}
