namespace YoumaconSecurityOps.Core.Shared.Context.Configurations;

public partial class StaffTypesConfiguration : IEntityTypeConfiguration<StaffType>
{
    public void Configure(EntityTypeBuilder<StaffType> entity)
    {
        entity.ToTable("StaffTypes");

        entity.HasKey(e => e.Id)
            .HasName("PK_StaffTypes");

        entity.Property(e => e.Title)
            .HasColumnName("Title");

        entity.Property(e => e.Description)
            .HasColumnName("Description");

        entity.HasMany(st => st.StaffTypeRoleMaps)
            .WithOne(str => str.StaffTypeNavigation)
            .HasForeignKey("FK_StaffTypesRoles_StaffTypes");

        entity.Ignore(e => e.StaffTypeRoleMaps);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<StaffRole> entity);
}
