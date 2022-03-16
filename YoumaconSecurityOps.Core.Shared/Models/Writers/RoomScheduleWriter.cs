namespace YoumaconSecurityOps.Core.Shared.Models.Writers;

public record RoomScheduleWriter(Guid LastStaffInRoomId, Boolean IsCurrentlyOccupied, String RoomNumber,
    Int32 NumberOfKeys, Guid LocationId) : BaseWriter;

