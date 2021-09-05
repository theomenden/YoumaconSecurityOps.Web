using System.Collections.Generic;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    /// <summary>
    /// <para>Empty query class for <see cref="StaffType" /></para>
    /// <inheritdoc ref="QueryBase" />
    /// </summary>
    public class GetStaffTypesQuery : QueryBase<IAsyncEnumerable<StaffType>>
    {
    }
}
