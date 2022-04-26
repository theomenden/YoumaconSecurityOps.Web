namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("RoomSchedule")]
public partial class RoomScheduleReader : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public bool IsCurrentlyOccupied { get; set; }

    public int Floor { get; set; }

    public int Number { get; set; }

    [StringLength(30)]
    public string Name { get; set; } = null!;
    
    public int Keys { get; set; }
    
    public int ProvidedKeys { get; set; }

    public Guid Location_Id { get; set; }
}