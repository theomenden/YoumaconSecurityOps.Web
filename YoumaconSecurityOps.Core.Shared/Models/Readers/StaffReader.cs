namespace YoumaconSecurityOps.Core.Shared.Models.Readers;
[Table("Staff")]
public partial class StaffReader : BaseReader, IEquatable<StaffReader>
{
    public StaffReader()
    {
    }

    #region BaseProperties
    public Guid ContactId { get; set; }

    public Guid StaffTypeRoleId { get; set; }

    public bool IsOnBreak { get; set; }

    [Required] public bool? NeedsCrashSpace { get; set; }

    public bool IsBlackShirt { get; set; }

    public bool IsRaveApproved { get; set; }

    public DateTime? BreakStartAt { get; set; }

    public DateTime? BreakEndAt { get; set; }

    [NotMapped]
    public StaffRole? Role => StaffTypeRoleMaps.Any() ? StaffTypeRoleMaps.Max(r => r.Role) : null;

    [NotMapped]
    public StaffType? StaffType => StaffTypeRoleMaps.Any() ? StaffTypeRoleMaps.Max(r => r.StaffTypeNavigation) : null;

    [StringLength(6)] public string ShirtSize { get; set; }

    #endregion

    #region Navigation Properties

    [ForeignKey(nameof(ContactId))]
    [InverseProperty(nameof(ContactReader.StaffMember))]
    public virtual ContactReader Contact { get; set; }


    [InverseProperty(nameof(IncidentReader.RecordedBy))]
    public virtual ICollection<IncidentReader> IncidentRecordedBy { get; set; } = new HashSet<IncidentReader>();

    [InverseProperty(nameof(IncidentReader.ReportedBy))]
    public virtual ICollection<IncidentReader> IncidentReportedBy { get; set; } = new HashSet<IncidentReader>();

    [InverseProperty(nameof(RadioScheduleReader.LastStaffToHave))]
    public virtual ICollection<RadioScheduleReader> RadioSchedules { get; set; } = new HashSet<RadioScheduleReader>();

    [InverseProperty(nameof(RoomScheduleReader.LastStaffInRoom))]
    public virtual ICollection<RoomScheduleReader> RoomSchedules { get; set; } = new HashSet<RoomScheduleReader>();

    [InverseProperty(nameof(ShiftReader.StaffMember))]
    public virtual ICollection<ShiftReader> Shifts { get; set; } = new HashSet<ShiftReader>();

    [InverseProperty(nameof(StaffTypesRoles.Staff))]
    public virtual ICollection<StaffTypesRoles> StaffTypeRoleMaps { get; set; } = new HashSet<StaffTypesRoles>();
    #endregion

    #region Overrides
    public bool Equals(StaffReader other)
    {
        if (other is null)
        {
            return false;
        }

        return other.Id == Id;
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj is StaffReader other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 47;
    }
    #endregion
}

