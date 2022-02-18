namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record ShiftReportInCommand(Guid ShiftId, Guid CurrentLocationId) : ICommand<Guid>
{
    public Guid Id => Guid.NewGuid();
}