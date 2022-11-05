namespace YoumaconSecurityOps.Core.Shared.Models.DTO;
public sealed record StaffDto(Guid Id, ContactDto ContactInformation, Boolean IsOnBreak, Boolean IsBlackShirt, Boolean IsRaveApproved, String ShirtSize, IEnumerable<RadioDto> Radios, IEnumerable<ShiftDto> Shifts, IEnumerable<RoleDto> Roles);
