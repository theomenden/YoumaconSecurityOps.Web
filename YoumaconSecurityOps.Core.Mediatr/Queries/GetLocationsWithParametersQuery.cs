namespace YoumaconSecurityOps.Core.Mediatr.Queries;

public record GetLocationsWithParametersQuery(LocationQueryStringParameters Parameters) : StreamQueryBase<LocationReader>;