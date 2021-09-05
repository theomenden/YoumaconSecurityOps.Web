using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    /// <summary>
    /// <para>Allows for <see cref="IncidentQueryStringParameters"/> to be used when searching for incidents</para>
    /// </summary>
    /// <remarks><inheritdoc cref="QueryBase{T}"/></remarks>
    public class GetIncidentsWithParametersQuery: QueryBase<IAsyncEnumerable<IncidentReader>>
    {
        public GetIncidentsWithParametersQuery(IncidentQueryStringParameters parameters)
        {
            Parameters = parameters;
        }

        /// <value>
        /// Parameters to be used in the query
        /// </value>
        public IncidentQueryStringParameters Parameters { get; }
    }
}
