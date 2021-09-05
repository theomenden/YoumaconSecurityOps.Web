using System.Collections.Generic;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    public class GetStaffWithParametersQuery:QueryBase<IAsyncEnumerable<StaffReader>>
    {
        public GetStaffWithParametersQuery(StaffQueryStringParameters parameters)
        {
            Parameters = parameters;
        }

        public StaffQueryStringParameters Parameters { get;}
    }
}
