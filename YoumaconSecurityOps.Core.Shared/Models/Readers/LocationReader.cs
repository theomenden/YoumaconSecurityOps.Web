namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("Locations")]
public partial class LocationReader: IEntity
{
    public LocationReader()
    {
        Incidents = new HashSet<IncidentReader>();
        RadioSchedules = new HashSet<RadioScheduleReader>();
        ShiftCurrentLocations = new HashSet<ShiftReader>();
        AssociatedShifts = new HashSet<ShiftReader>();
    }

    [Key]
    public Guid Id { get; set; }
    
    [StringLength(100)]
    public string Name { get; set; } = null!;
    
    public bool IsHotel { get; set; }

    [InverseProperty(nameof(IncidentReader.Location))]
    public virtual ICollection<IncidentReader> Incidents { get; set; }
    
    [InverseProperty(nameof(RadioScheduleReader.Location))]
    public virtual ICollection<RadioScheduleReader> RadioSchedules { get; set; }
    
    [InverseProperty(nameof(ShiftReader.CurrentLocation))]
    public virtual ICollection<ShiftReader> ShiftCurrentLocations { get; set; }

    [InverseProperty(nameof(ShiftReader.StartingLocation))]
    public virtual ICollection<ShiftReader> AssociatedShifts { get; set; }
}