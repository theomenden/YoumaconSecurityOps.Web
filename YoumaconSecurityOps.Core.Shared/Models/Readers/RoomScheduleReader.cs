namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("RoomSchedule")]
[Index(nameof(Location_Id), Name = "IX_RoomSchedule_Location")]
public partial class RoomScheduleReader : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public bool IsCurrentlyOccupied { get; set; }
    [StringLength(10)]
    public string RoomNumber { get; set; } = null!;
    public int NumberOfKeys { get; set; }
    public Guid Location_Id { get; set; }
}