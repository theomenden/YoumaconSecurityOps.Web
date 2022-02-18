namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddLocationCommand(string Name, bool IsHotel) : ICommand
{
    public Guid Id => Guid.NewGuid();
}