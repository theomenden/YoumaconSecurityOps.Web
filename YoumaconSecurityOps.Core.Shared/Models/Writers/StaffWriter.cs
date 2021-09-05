using System;

namespace YoumaconSecurityOps.Core.Shared.Models.Writers
{
    public record StaffWriter(Guid ContactId, int RoleId,
        int StaffTypeId, bool NeedsCrashSpace, bool IsBlackShirt, bool IsRaveApproved
        , string ShirtSize) : BaseWriter;
}
