namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddShiftCommandWithReturn(DateTime StartAt, DateTime EndAt, Guid StaffMemberId, String StaffMemberName,
    Guid StartingLocationId) : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}