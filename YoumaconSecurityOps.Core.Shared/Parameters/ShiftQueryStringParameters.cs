using System;
using System.Collections.Generic;

namespace YoumaconSecurityOps.Core.Shared.Parameters
{
    public record ShiftQueryStringParameters(IEnumerable<Guid> StaffIds, DateTime StartAt, DateTime EndAt) : QueryStringParameters;
}
