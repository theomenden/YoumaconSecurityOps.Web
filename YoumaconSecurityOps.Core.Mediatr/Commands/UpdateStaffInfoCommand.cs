namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record UpdateStaffInfoCommand() : ICommand<Guid>
{
    public Guid Id => Guid.NewGuid();
}