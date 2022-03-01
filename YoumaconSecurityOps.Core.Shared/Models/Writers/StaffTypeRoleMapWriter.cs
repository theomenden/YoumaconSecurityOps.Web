namespace YoumaconSecurityOps.Core.Shared.Models.Writers;

public record StaffTypeRoleMapWriter(Guid StaffId, Int32 StaffTypeId, Int32 RoleId) : BaseWriter;

