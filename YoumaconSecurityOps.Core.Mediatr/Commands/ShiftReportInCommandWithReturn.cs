namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record ShiftReportInCommandWithReturn : ICommandWithReturn<Guid>
{
    public ShiftReportInCommandWithReturn(Guid shiftId, Guid currentLocationId)
    {
        ShiftId = shiftId;

        CurrentLocationId = currentLocationId;

        Id = Guid.NewGuid();
    }
       public Guid ShiftId { get; }

       public Guid CurrentLocationId { get; }

       public Guid Id { get; }
}