namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

/// <summary>
/// Contains pronouns stored in the database
/// </summary>
[Table("Pronouns")]
public partial class Pronouns
{
    public Pronouns()
    {
        Contacts = new HashSet<ContactReader>(20);
    }

    /// <summary>
    /// Storage Id for Pronouns.
    /// </summary>
    [Key]
    public Int32 Id { get; set; }

    /// <value>
    /// The pronouns themselves
    /// </value>
    public string Name { get; set; }

    [InverseProperty(nameof(ContactReader.Pronouns))]
    public virtual ICollection<ContactReader> Contacts { get; set; }
}

