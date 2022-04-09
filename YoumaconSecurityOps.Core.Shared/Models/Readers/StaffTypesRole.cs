namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

public partial class StaffTypesRole : IEntity, IComparable<StaffTypesRole>, IEquatable<StaffTypesRole>
{
    [Key]
    public Guid Id { get; set; }

    public Guid StaffId { get; set; }

    public int StaffTypeId { get; set; }

    public int RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    [InverseProperty("StaffTypesRoles")]
    public virtual StaffRole StaffRole { get; set; } = null!;

    [ForeignKey(nameof(StaffId))]
    [InverseProperty("StaffTypesRoles")]
    public virtual StaffReader Staff { get; set; } = null!;

    [ForeignKey(nameof(StaffTypeId))]
    [InverseProperty("StaffTypesRoles")]
    public virtual StaffType StaffType { get; set; } = null!;

    public int CompareTo(StaffTypesRole? other)
    {
        return other is not null ? other.RoleId.CompareTo(RoleId) : 1;
    }

    public bool Equals(StaffTypesRole? other)
    {
        return other is not null
               && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj is StaffTypesRole other && Equals(other);
    }

    public override int GetHashCode()
    {
        return StaffTypeId.GetHashCode() ^ RoleId.GetHashCode() ^ Id.GetHashCode();
    }
}