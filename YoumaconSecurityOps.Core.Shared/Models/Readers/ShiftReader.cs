namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Index(nameof(CheckedInAt), nameof(CheckedOutAt), Name = "IX_Shifts_CheckedInAt_CheckedOutAt")]
[Index(nameof(StaffId), Name = "IX_Shifts_StaffId")]
[Index(nameof(StartAt), nameof(EndAt), Name = "IX_Shifts_StartAt_EndAt")]
public partial class ShiftReader : IEntity
{
    public ShiftReader()
    {
        Incidents = new HashSet<IncidentReader>();
    }

    [Key]
    public Guid Id { get; set; }
    
    public Guid StaffId { get; set; }
    
    public DateTime EndAt { get; set; }
    
    public DateTime StartAt { get; set; }
    
    public Guid StartingLocationId { get; set; }
    
    public Guid CurrentLocationId { get; set; }
    
    public DateTime? CheckedInAt { get; set; }
    
    public DateTime? CheckedOutAt { get; set; }
    
    public DateTime? LastReportedAt { get; set; }

    [NotMapped]
    public Boolean IsLate => CheckedInAt.HasValue && CheckedInAt.Value >= StartAt.AddMinutes(15);

    [StringLength(500)]
    public string? Notes { get; set; }

    [ForeignKey(nameof(CurrentLocationId))]
    [InverseProperty(nameof(LocationReader.ShiftCurrentLocations))]
    public virtual LocationReader CurrentLocation { get; set; } = null!;
    
    [ForeignKey(nameof(StaffId))]
    [InverseProperty("Shifts")]
    public virtual StaffReader StaffMember { get; set; } = null!;
    
    [ForeignKey(nameof(StartingLocationId))]
    [InverseProperty(nameof(LocationReader.AssociatedShifts))]
    public virtual LocationReader StartingLocation { get; set; } = null!;
    
    [InverseProperty(nameof(IncidentReader.ShiftReader))]
    public virtual ICollection<IncidentReader> Incidents { get; set; }
}