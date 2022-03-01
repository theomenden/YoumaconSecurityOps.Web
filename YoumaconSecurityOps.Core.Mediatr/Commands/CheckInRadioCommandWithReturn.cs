namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record CheckInRadioCommandWithReturn(Guid RadioId) : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}