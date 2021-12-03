using System;
using System.Collections.Generic;

namespace YoumaconSecurityOps.Core.Shared.Parameters
{
    public record RadioScheduleQueryStringParameter(IEnumerable<String> RadioIds, IEnumerable<Guid> StaffMemberIds,
        IEnumerable<Guid> LocationIds) : QueryStringParameters;
}
