namespace YoumaconSecurityOps.Core.Shared.Models.Writers;

public record StaffWriter(Int32 RoleId,
    Int32 StaffTypeId, Boolean NeedsCrashSpace, Boolean IsBlackShirt, Boolean IsRaveApproved
    , String ShirtSize) : BaseWriter;