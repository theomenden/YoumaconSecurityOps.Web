using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    public record GetStaffWithParametersQuery(StaffQueryStringParameters Parameters): IQuery<IAsyncEnumerable<StaffReader>>
    {
        public Guid Id => Guid.NewGuid();
    }
}
