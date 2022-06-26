namespace YsecOps.Core.Mediator.Requests.Queries.Streaming;

public sealed record GetStaffMembersWithParametersQuery
    (StaffListQueryParameters Parameters) : IStreamRequest<Staff>;

public sealed record GetStaffMembersAtLocationsQuery
    (StaffLocationQueryParameters Parameters) : IStreamRequest<Staff>;

