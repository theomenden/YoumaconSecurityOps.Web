namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddStaffCommand: ICommand
{
    public Guid Id => Guid.NewGuid();
}