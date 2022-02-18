namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("RoomSchedule")]
[Index(nameof(LocationId), Name = "IX_RoomSchedule_Location")]
public class RoomScheduleReader: BaseReader
{
    public DateTime SysStart { get; set; }

    public DateTime SysEnd { get; set; }

    public Guid LastStaffInRoomId { get; set; }

    public bool IsCurrentlyOccupied { get; set; }

    public string RoomNumber { get; set; }

    public int NumberOfKeys { get; set; }

    public Guid LocationId { get; set; }

    [ForeignKey(nameof(LastStaffInRoomId))]
    [InverseProperty(nameof(StaffReader.RoomSchedules))]
    public virtual StaffReader LastStaffInRoom { get; set; } = default!;
}