using System;

namespace YoumaconSecurityOps.Core.Shared.Models.Writers
{
    public record ShiftWriter(DateTime StartAt, DateTime EndAt, Guid StaffMemberId, String StaffMemberName,
        Guid StartingLocationId) : BaseWriter;
}
