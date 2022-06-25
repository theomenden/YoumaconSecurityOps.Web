namespace YsecOps.Core.Mediator.Requests.Queries.Streaming;

    public sealed record GetStaffMembersWithParametersQuery
        (StaffListQueryParameters Parameters) : IStreamRequest<Staff>;

