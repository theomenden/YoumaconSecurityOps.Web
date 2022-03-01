namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record SendOnBreakCommandWithReturn(Guid StaffId) : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}