using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Parameters;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    public class GetRadioScheduleWithParameters: QueryBase<IAsyncEnumerable<RadioScheduleReader>>
    {
        public GetRadioScheduleWithParameters(RadioScheduleQueryStringParameter parameters)
        {
            Parameters = parameters;
        }

        public RadioScheduleQueryStringParameter Parameters { get; }
    }
}
