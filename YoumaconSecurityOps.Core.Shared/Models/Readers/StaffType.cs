namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

public partial class StaffType
{
    public StaffType()
    {
        StaffTypesRoles = new HashSet<StaffTypesRole>();
    }

    [Key]
    public int Id { get; set; }
    [StringLength(20)]
    public string Title { get; set; } = null!;
    [StringLength(100)]
    public string Description { get; set; } = null!;

    [InverseProperty(nameof(StaffTypesRole.StaffType))]
    public virtual ICollection<StaffTypesRole> StaffTypesRoles { get; set; }
}