namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record SendOnBreakCommand(Guid StaffId) : ICommand<Guid>
{
    public Guid Id => Guid.NewGuid();
}