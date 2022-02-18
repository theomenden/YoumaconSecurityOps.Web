using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YoumaconSecurityOps.Data.EntityFramework.Context.Configurations
{
    public partial class StaffRoleConfiguration: IEntityTypeConfiguration<StaffRole>
    {
        public void Configure(EntityTypeBuilder<StaffRole> entity)
        {
            entity.ToTable("Roles");

            entity.HasKey(e => e.Id)
                .HasName("PK_Roles");

            entity.Property(e => e.Name)
                .HasColumnName("Role");
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StaffRole> entity);
    }
}
