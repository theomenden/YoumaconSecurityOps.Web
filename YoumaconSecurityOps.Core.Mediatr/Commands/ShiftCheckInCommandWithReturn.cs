namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record ShiftCheckInCommandWithReturn(Guid ShiftId): ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}