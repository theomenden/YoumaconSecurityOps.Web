namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record ShiftReportInCommandWithReturn(Guid ShiftId, Guid CurrentLocationId) : ICommandWithReturn<Guid>
{
    public Guid Id => Guid.NewGuid();
}