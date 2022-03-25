namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

/// <summary>
/// Role of the staff member.
/// <inheritdoc cref="IEquatable{T}"/>
/// <inheritdoc cref="IComparable{T}"/>
/// </summary>
public partial class StaffRole: IEquatable<StaffRole>, IComparable<StaffRole>
{
    /// <summary>
    /// Defines the rank of the staff member
    /// </summary>
    public StaffRole()
    {
    }

    [Key]
    public int Id { get; set; }

    [Column("Role")]
    [StringLength(50)]
    public string Name { get; set; }
        
    [InverseProperty(nameof(StaffTypesRoles.Role))]
    public virtual ICollection<StaffTypesRoles> StaffTypeRoleMap { get; set; } = new HashSet<StaffTypesRoles>(20);

    #region Overrides
    public bool Equals(StaffRole other)
    {
        return other is not null
               && other.Id == Id
               || (
                   !String.IsNullOrWhiteSpace(other?.Name)
                   && other.Name.Equals(Name)
               );
    }

    public int CompareTo(StaffRole other)
    {
        return Id.CompareTo(other?.Id);
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

        return obj is StaffRole other && Equals(other);
    }

    public override int GetHashCode()
    {
        return 47 * Id.GetHashCode();
    }

    public static bool operator ==(StaffRole left, StaffRole right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(StaffRole left, StaffRole right)
    {
        return !(left == right);
    }

    public static bool operator <(StaffRole left, StaffRole right)
    {
        return left is not null && left.CompareTo(right) < 0;
    }

    public static bool operator <=(StaffRole left, StaffRole right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(StaffRole left, StaffRole right)
    {
        return left?.CompareTo(right) > 0;
    }

    public static bool operator >=(StaffRole left, StaffRole right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
    #endregion
}