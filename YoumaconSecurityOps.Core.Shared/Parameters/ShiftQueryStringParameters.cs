namespace YoumaconSecurityOps.Core.Shared.Parameters;

public record ShiftQueryStringParameters(IEnumerable<Guid> StaffIds, DateTime StartAt, DateTime EndAt) : QueryStringParameters;