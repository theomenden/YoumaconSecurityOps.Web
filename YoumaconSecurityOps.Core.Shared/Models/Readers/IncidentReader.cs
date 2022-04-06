namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("Incidents")]
[Index(nameof(OpsManager_Id), Name = "IX_Incidents_OpsManagerId")]
[Index(nameof(RecordedBy_Id), Name = "IX_Incidents_RecordedBy")]
[Index(nameof(RecordedOn), nameof(Severity), Name = "IX_Incidents_Severity")]
public partial class IncidentReader : IEntity
{
    public IncidentReader()
    {
        NonStaffPeoples = new HashSet<NonStaffPeople>();
    }

    [Key]
    public Guid Id { get; set; }
    
    public Guid RecordedBy_Id { get; set; }
    
    public Guid OpsManager_Id { get; set; }
    
    public Guid Shift_Id { get; set; }
    
    public Guid Location_Id { get; set; }
    
    public Severity Severity { get; set; }
    
    public DateTime RecordedOn { get; set; }
    
    [StringLength(1000)]
    public string Title { get; set; } = null!;
    
    [StringLength(1000)]
    public string Description { get; set; } = null!;
    
    [StringLength(500)]
    public string Keywords { get; set; } = null!;
    
    public bool IsFollowUpRequired { get; set; }
    
    [StringLength(1000)]
    public string? FollowUpResponse { get; set; }
    
    [StringLength(1000)]
    public string? Injuries { get; set; }
    
    public DateTime? ResolvedAt { get; set; }

    [ForeignKey(nameof(Location_Id))]
    [InverseProperty("Incidents")]
    public virtual LocationReader Location { get; set; } = null!;
    
    [ForeignKey(nameof(OpsManager_Id))]
    [InverseProperty(nameof(StaffReader.IncidentOpsManagers))]
    public virtual StaffReader OpsManager { get; set; } = null!;
    
    [ForeignKey(nameof(RecordedBy_Id))]
    [InverseProperty(nameof(StaffReader.IncidentRecordedBies))]
    public virtual StaffReader RecordedBy { get; set; } = null!;
    
    [ForeignKey(nameof(Shift_Id))]
    [InverseProperty("Incidents")]
    public virtual ShiftReader ShiftReader { get; set; } = null!;
    
    [InverseProperty(nameof(NonStaffPeople.IncidentReader))]
    public virtual ICollection<NonStaffPeople> NonStaffPeoples { get; set; }
}