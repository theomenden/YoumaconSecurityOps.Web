namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddRadioCommand(Guid StartingLocationId, String RadioNumber) : ICommand<Guid>
{
    public Guid Id => Guid.NewGuid();
}