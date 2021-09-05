using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Enumerations;

namespace YoumaconSecurityOps.Core.Shared.Parameters
{
    public record IncidentQueryStringParameters(IEnumerable<Guid> IncidentIds, IEnumerable<Guid> ShiftIds, IEnumerable<Guid> StaffIds, Severity Severity, String Title) : QueryStringParameters;
}
