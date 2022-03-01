namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record ShiftCheckoutCommandWithReturn(Guid ShiftId) : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}