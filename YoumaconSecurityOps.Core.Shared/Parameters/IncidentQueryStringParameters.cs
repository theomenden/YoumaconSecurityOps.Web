namespace YoumaconSecurityOps.Core.Shared.Parameters;

public record IncidentQueryStringParameters(IEnumerable<Guid> IncidentIds, IEnumerable<Guid> ShiftIds, IEnumerable<Guid> StaffIds, Severity Severity, String Title) : QueryStringParameters;