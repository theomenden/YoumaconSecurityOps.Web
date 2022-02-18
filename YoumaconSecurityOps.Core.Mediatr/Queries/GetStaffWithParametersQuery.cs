namespace YoumaconSecurityOps.Core.Mediatr.Queries;

public record GetStaffWithParametersQuery(StaffQueryStringParameters Parameters): StreamQueryBase<StaffReader>;