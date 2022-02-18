namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record CheckInRadioCommand(Guid RadioId) : ICommand<Guid>
{
    public Guid Id => Guid.NewGuid();
}