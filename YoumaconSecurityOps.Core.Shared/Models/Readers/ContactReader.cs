namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Index(nameof(LastName), nameof(FirstName), Name = "IX_Contacts_LastName_FirstName")]
[Index(nameof(Pronoun_Id), Name = "IX_Contacts_Pronoun_Id")]
[Index(nameof(Staff_Id), Name = "IX_Contacts_StaffId")]
public partial class ContactReader : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid Staff_Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public int Pronoun_Id { get; set; }
    [StringLength(100)]
    public string FirstName { get; set; } = null!;
    [StringLength(100)]
    public string LastName { get; set; } = null!;
    public long PhoneNumber { get; set; }
    [StringLength(50)]
    public string Email { get; set; } = null!;
    [StringLength(100)]
    public string? FacebookName { get; set; }
    [StringLength(100)]
    public string? PreferredName { get; set; }

    [ForeignKey(nameof(Pronoun_Id))]
    [InverseProperty("Contacts")]
    public virtual Pronoun Pronoun { get; set; } = null!;
    
    [ForeignKey(nameof(Staff_Id))]
    [InverseProperty("ContactInformation")]
    public virtual StaffReader Staff { get; set; } = null!;
}