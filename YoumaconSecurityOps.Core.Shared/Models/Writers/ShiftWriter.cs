using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Shared.Models.Writers
{
    public record ShiftWriter(DateTime StartAt, DateTime EndAt, StaffReader StaffMember,
        LocationReader StartingLocation) : BaseWriter;
}
