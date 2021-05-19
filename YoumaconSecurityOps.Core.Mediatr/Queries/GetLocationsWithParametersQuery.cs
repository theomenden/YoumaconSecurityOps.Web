using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    public class GetLocationsWithParametersQuery: QueryBase<IAsyncEnumerable<LocationReader>>
    {
        public GetLocationsWithParametersQuery(LocationQueryStringParameters parameters)
        {
            Parameters = parameters;
        }

        public LocationQueryStringParameters Parameters { get; }
    }
}
