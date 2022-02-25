namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

/// <summary>
/// <para>Contact information for a particular staff member</para>
/// <inheritdoc cref="IEquatable{T}"/>
/// </summary>
[Table("Contacts")]
[Index(nameof(LastName), nameof(FirstName), Name = "IX_Contacts_LastName_FirstName")]
[Index(nameof(Staff_Id), Name = "IX_Contacts_StaffId", IsUnique = true)]
public class ContactReader : BaseReader, IEquatable<ContactReader>
{
    public ContactReader()
    {
    }

    public Guid Staff_Id { get; set; }

    [ConcurrencyCheck]
    public DateTime CreatedOn { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = default!;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = default!;

    public long PhoneNumber { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; } = default!;

    [StringLength(100)]
    public string FacebookName { get; set; }

    [StringLength(100)]
    public string PreferredName { get; set; }

    [ForeignKey(nameof(Staff_Id))]
    [InverseProperty(nameof(StaffReader.Contact))]
    public virtual StaffReader StaffMember { get; set; }

    #region Overrides
    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        return obj is ContactReader other && Equals(other);
    }

    public override int GetHashCode()
    {
        return 37 * Id.GetHashCode();
    }

    public bool Equals(ContactReader other)
    {
        return other is not null
               && (
                   PhoneNumber == other.PhoneNumber
                   || (!String.IsNullOrWhiteSpace(other.Email) && other.Email.Equals(Email))
                   || Id == other.Id
               );
    }

    public static bool operator ==(ContactReader lhs, ContactReader rhs)
    {
        return lhs?.Equals(rhs) ?? rhs is null;
    }

    public static bool operator !=(ContactReader lhs, ContactReader rhs)
    {
        return !(lhs == rhs);
    }
    #endregion
}