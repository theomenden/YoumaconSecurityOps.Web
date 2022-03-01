namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddRadioCommandWithReturn(Guid StartingLocationId, String RadioNumber) : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}