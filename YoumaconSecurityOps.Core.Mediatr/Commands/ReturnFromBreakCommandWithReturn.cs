namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record ReturnFromBreakCommandWithReturn(Guid StaffId) : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}