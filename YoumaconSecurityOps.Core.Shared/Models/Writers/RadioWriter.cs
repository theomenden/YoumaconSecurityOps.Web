using System;

namespace YoumaconSecurityOps.Core.Shared.Models.Writers
{
    public record RadioWriter(Guid LastStaffToHaveId, Guid LocationId, String RadioNumber) : BaseWriter;
}                             