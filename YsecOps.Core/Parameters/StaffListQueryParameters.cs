namespace YoumaconSecurityOps.Core.Shared.Parameters;
public sealed record StaffListQueryParameters(IEnumerable<Guid> StaffIds,Boolean IsBlackShirt, Boolean IsRaveApproved, Boolean NeedsCrashSpace) : QueryStringParameters;

public sealed record StaffLocationQueryParameters(IEnumerable<Guid> LocationIds, Boolean IsBlackShirt, Boolean IsRaveApproved, Boolean NeedsCrashSpace): QueryStringParameters;