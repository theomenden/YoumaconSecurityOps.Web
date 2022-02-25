namespace YoumaconSecurityOps.Core.Shared.Context.Configurations;

public partial class StaffRoleConfiguration : IEntityTypeConfiguration<StaffRole>
{
    public void Configure(EntityTypeBuilder<StaffRole> entity)
    {
        entity.ToTable("Roles");

        entity.HasKey(e => e.Id)
            .HasName("PK_Roles");

        entity.Property(e => e.Name)
            .HasColumnName("Role");

        entity.HasMany(sr => sr.StaffTypeRoleMap)
            .WithOne(str => str.Role)
            .HasForeignKey("FK_StaffTypesRoles_Roles");

        entity.Ignore(e => e.StaffTypeRoleMap);

    }

    partial void OnConfigurePartial(EntityTypeBuilder<StaffRole> entity);
}