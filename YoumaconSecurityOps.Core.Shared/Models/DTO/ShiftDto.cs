namespace YoumaconSecurityOps.Core.Shared.Models.DTO;
public sealed record ShiftDto(Guid Id, StaffDto Staff, DateTime CheckedInAt, DateTime CheckedOutAt, Boolean IsLateToStart, LocationDto CurrentLocation, IEnumerable<IncidentDto> Incidents);
