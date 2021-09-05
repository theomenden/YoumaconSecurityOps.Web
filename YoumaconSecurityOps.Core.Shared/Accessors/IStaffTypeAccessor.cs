using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Shared.Accessors
{
    public interface IStaffTypeAccessor: IAsyncEnumerable<StaffType>
    {
        IAsyncEnumerable<StaffType> GetAll(CancellationToken cancellationToken = default);

        Task<StaffType> WithId(Int32 staffTypeId, CancellationToken cancellationToken = default);
    }
}
