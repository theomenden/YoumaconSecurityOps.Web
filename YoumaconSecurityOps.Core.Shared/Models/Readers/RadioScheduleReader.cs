namespace YoumaconSecurityOps.Core.Shared.Models.Readers;


[Table("RadioSchedule")]
[Index(nameof(LastStaffToHave_Id), Name = "IX_RadioSchedule_LastStaffId")]
[Index(nameof(Location_Id), Name = "IX_RadioSchedule_Location")]
[Index(nameof(RadioNumber), Name = "IX_RadioSchedule_RadioNumber")]
public partial class RadioScheduleReader : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid LastStaffToHave_Id { get; set; }
    public Guid Location_Id { get; set; }
    [StringLength(20)]
    public string RadioNumber { get; set; } = null!;
    public DateTime? CheckedOutAt { get; set; }
    public DateTime? CheckedInAt { get; set; }
    public bool IsCharging { get; set; } = true;

    [NotMapped]
    public bool IsCheckedIn => CheckedInAt.HasValue;

    [ForeignKey(nameof(LastStaffToHave_Id))]
    [InverseProperty(nameof(StaffReader.RadioSchedules))]
    public virtual StaffReader LastStaffToHave { get; set; } = null!;
    [ForeignKey(nameof(Location_Id))]
    [InverseProperty("RadioSchedules")]
    public virtual LocationReader Location { get; set; } = null!;
}