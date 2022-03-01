namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record UpdateStaffInfoCommandWithReturn() : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}