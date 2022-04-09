namespace YoumaconSecurityOps.Core.Shared.Models.Writers;

public record FullStaffWriter(StaffWriter StaffWriter, ContactWriter ContactWriter, StaffTypeRoleMapWriter StaffTypeRoleMapWriter) : BaseWriter;