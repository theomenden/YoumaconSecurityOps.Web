namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record UpdateShiftLocationCommandWithReturn : ICommandWithReturn<ShiftReader>
{
    public UpdateShiftLocationCommandWithReturn(Guid shiftId, Guid locationId)
    {
        LocationId = locationId;
        Id = Guid.NewGuid();
        ShiftId = shiftId;
    }

    public Guid Id { get; }

    public Guid ShiftId { get; }

    public Guid LocationId { get; }
}