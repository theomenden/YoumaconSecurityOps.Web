namespace YoumaconSecurityOps.Core.Mediatr.Queries;

public record GetShiftListWithParametersQuery(ShiftQueryStringParameters Parameters) : StreamQueryBase<ShiftReader>;