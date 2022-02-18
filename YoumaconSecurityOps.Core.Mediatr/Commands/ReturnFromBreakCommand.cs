namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record ReturnFromBreakCommand(Guid StaffId) : ICommand<Guid>
{
    public Guid Id => Guid.NewGuid();
}