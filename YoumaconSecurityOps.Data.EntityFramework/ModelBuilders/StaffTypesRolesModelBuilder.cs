using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Data.EntityFramework.ModelBuilders
{
    internal class StaffTypesRolesModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<StaffTypesRoles> entity)
        {
            entity.ToTable("StaffTypesRoles");

            entity.HasKey(e => e.Id)
                .IsClustered(false);

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.StaffTypesRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffTypesRoles_Roles");

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffTypesRoles)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffTypesRoles_Staff");

            entity.HasOne(d => d.StaffTypeNavigation)
                .WithMany(p => p.StaffTypesRoles)
                .HasForeignKey(d => d.StaffType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffTypesRoles_StaffTypes");
        }
    }
}
