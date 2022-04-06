namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

public partial class Pronoun
{
    public Pronoun()
    {
        Contacts = new HashSet<ContactReader>();
        NonStaffPeoples = new HashSet<NonStaffPeople>();
    }

    [Key]
    public int Id { get; set; }
    [StringLength(40)]
    public string Name { get; set; } = null!;

    [InverseProperty(nameof(ContactReader.Pronoun))]
    public virtual ICollection<ContactReader> Contacts { get; set; }
    [InverseProperty(nameof(NonStaffPeople.Pronoun))]
    public virtual ICollection<NonStaffPeople> NonStaffPeoples { get; set; }
}