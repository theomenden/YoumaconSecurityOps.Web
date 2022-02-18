namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record ShiftCheckInCommand(Guid ShiftId): ICommand<Guid>
{
    public Guid Id => Guid.NewGuid();
}