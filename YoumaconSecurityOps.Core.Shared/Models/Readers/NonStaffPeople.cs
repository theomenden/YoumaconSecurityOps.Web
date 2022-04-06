namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("NonStaffPeople")]
[Index(nameof(IncidentId), Name = "IX_NonStaffPeople_IncidentId")]
[Index(nameof(LastName), nameof(FirstName), Name = "IX_NonStaffPeople_LastName_FirstName")]
[Index(nameof(PronounId), Name = "IX_NonStaffPeople_PronounId")]
public partial class NonStaffPeople : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid IncidentId { get; set; }
    [StringLength(50)]
    public string FirstName { get; set; } = null!;
    [StringLength(50)]
    public string LastName { get; set; } = null!;
    [StringLength(50)]
    public string Alias { get; set; } = null!;
    public long PhoneNumber { get; set; }
    public int PronounId { get; set; }
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [ForeignKey(nameof(IncidentId))]
    [InverseProperty("NonStaffPeoples")]
    public virtual IncidentReader IncidentReader { get; set; } = null!;
    [ForeignKey(nameof(PronounId))]
    [InverseProperty("NonStaffPeoples")]
    public virtual Pronoun Pronoun { get; set; } = null!;
}