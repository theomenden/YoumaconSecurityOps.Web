using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Parameters
{
    public record StaffQueryStringParameters(int RoleId, int TypeId, bool IsRaveApproved, bool NeedsCrashSpace,
        bool IsBlackShirt, bool IsOnBreak) : QueryStringParameters;
}
