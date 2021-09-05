using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Shared.Accessors
{
    public interface IStaffRoleAccessor: IAsyncEnumerable<StaffRole>
    {
        public IAsyncEnumerable<StaffRole> GetAll(CancellationToken cancellationToken = default);

        public Task<StaffRole> WithId(Int32 staffRoleId);
    }
}
