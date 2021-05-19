using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Parameters
{
    public record ShiftQueryStringParameters(IEnumerable<Guid> StaffIds, DateTime StartAt, DateTime EndAt) : QueryStringParameters;
}
