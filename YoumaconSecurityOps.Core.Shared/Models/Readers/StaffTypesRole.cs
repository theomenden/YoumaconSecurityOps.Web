namespace YoumaconSecurityOps.Core.Shared.Models.Readers;

public partial class StaffTypesRole : IEntity
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
}