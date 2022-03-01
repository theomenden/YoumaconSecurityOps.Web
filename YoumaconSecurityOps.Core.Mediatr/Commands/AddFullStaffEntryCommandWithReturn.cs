namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddFullStaffEntryCommandWithReturn(StaffWriter StaffWriter, ContactWriter ContactWriter) : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}