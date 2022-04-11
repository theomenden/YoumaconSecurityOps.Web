namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

public partial class Pronoun
{
    public Pronoun()
    {
    }

    [Key]
    public int Id { get; set; }
    [StringLength(40)]
    public string Name { get; set; } = null!;

    [InverseProperty(nameof(ContactReader.Pronoun))]
    public virtual ICollection<ContactReader> Contacts { get; set; } = new HashSet<ContactReader>(5);

    [InverseProperty(nameof(NonStaffPeople.Pronoun))]
    public virtual ICollection<NonStaffPeople> NonStaffPeoples { get; set; } = new HashSet<NonStaffPeople>(5);
}