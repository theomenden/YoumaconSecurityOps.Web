using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Data.EntityFramework.ModelBuilders
{
    class StaffModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<StaffReader> entity)
        {
            entity.ToTable("Staff");

            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.ContactId, "IX_Staff_Contact_Id");

            entity.HasIndex(e => e.RoleId, "IX_Staff_RoleId");

            entity.HasIndex(e => e.StaffTypeId, "IX_Staff_StaffType_Id");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            entity.Property(e => e.ContactId).HasColumnName("Contact_Id");

            entity.Property(e => e.NeedsCrashSpace)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.RoleId).HasColumnName("Role_Id");

            entity.Property(e => e.ShirtSize).HasMaxLength(6);

            entity.Property(e => e.StaffTypeId).HasColumnName("StaffType_Id");

            entity.HasOne(d => d.ContactInformation)
                .WithOne(p => p.StaffInformation)
                .HasForeignKey<StaffReader>(s => s.ContactId)
                .HasConstraintName("FK_Staff_Contacts_ContactId");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Staff)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Roles");

            entity.HasOne(d => d.StaffType)
                .WithMany(p => p.Staff)
                .HasForeignKey(d => d.StaffTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_StaffTypes");
        }
    }
}
