namespace YoumaconSecurityOps.Core.Mediatr.Commands;

public record AddRoomCommandWithReturn : ICommandWithReturn<Guid>
{
    public AddRoomCommandWithReturn(Guid staffId, string roomNumber, Guid locationId)
    {
        StaffId = staffId;
        RoomNumber = roomNumber;
        LocationId = locationId;
        Id = Guid.NewGuid();
    }

    public Guid Id { get;}

    public Guid StaffId { get;}

    public String RoomNumber { get; }

    public Guid LocationId { get; }
}