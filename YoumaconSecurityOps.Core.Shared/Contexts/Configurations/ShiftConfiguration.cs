﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>

#nullable disable

namespace YoumaconSecurityOps.Core.Shared.Contexts.Configurations;

public partial class ShiftConfiguration : IEntityTypeConfiguration<ShiftReader>
{
    public void Configure(EntityTypeBuilder<ShiftReader> entity)
    {
        entity.ToTable(tb => tb.IsTemporal(ttb =>
            {
                ttb.UseHistoryTable("Shifts_History", "dbo");
                ttb
                    .HasPeriodStart("SysStart")
                    .HasColumnName("SysStart");
                ttb
                    .HasPeriodEnd("SysEnd")
                    .HasColumnName("SysEnd");
            }
        ));

        entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

        entity.HasOne(d => d.CurrentLocation)
            .WithMany(p => p.ShiftCurrentLocations)
            .HasForeignKey(d => d.CurrentLocationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Shifts_CurrentLocation");

        entity.HasOne(d => d.StaffMember)
            .WithMany(p => p.Shifts)
            .HasForeignKey(d => d.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Shifts_Staff");

        entity.HasOne(d => d.StartingLocation)
            .WithMany(p => p.AssociatedShifts)
            .HasForeignKey(d => d.StartingLocationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Shifts_StartingLocation");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<ShiftReader> entity);
}