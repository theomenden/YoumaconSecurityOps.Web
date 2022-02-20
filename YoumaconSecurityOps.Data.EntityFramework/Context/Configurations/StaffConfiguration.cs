﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using YoumaconSecurityOps.Data.EntityFramework.Models;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

#nullable disable

namespace YoumaconSecurityOps.Data.EntityFramework.Context.Configurations
{
    public partial class StaffConfiguration : IEntityTypeConfiguration<StaffReader>
    {
        public void Configure(EntityTypeBuilder<StaffReader> entity)
        {
            entity.ToTable("Staff");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            entity.Property(e => e.NeedsCrashSpace).HasDefaultValueSql("((1))");

            entity.Property(e => e.ContactId)
                .HasColumnName("Contact_Id");

            entity.Property(e => e.StaffTypeRoleId)
                .HasColumnName("StaffTypeRoleId");

            entity.HasOne(d => d.Contact)
                .WithOne(p => p.StaffMember)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Contacts_ContactId");

            entity.HasMany(s => s.StaffTypeRoleMaps)
                .WithOne(p => p.Staff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_StaffTypesRoles_StaffTypeRoleId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StaffReader> entity);
    }
}