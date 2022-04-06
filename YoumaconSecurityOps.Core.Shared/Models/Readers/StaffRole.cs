namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

[Table("Roles")]
public partial class StaffRole
{
    public StaffRole()
    {
        StaffTypesRoles = new HashSet<StaffTypesRole>();
    }

    [Key]
    public int Id { get; set; }
    [Column("Role")]
    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty(nameof(StaffTypesRole.StaffRole))]
    public virtual ICollection<StaffTypesRole> StaffTypesRoles { get; set; }
}