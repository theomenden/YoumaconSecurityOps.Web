using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Data.EntityFramework.ModelBuilders
{
    internal class ShiftModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<ShiftReader> entity)
        {
            entity.ToTable("Shifts");

            entity.HasKey(e => e.Id)
                .HasName("PK_Shifts_Id");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.Property(e => e.SysEnd).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.SysStart).HasDefaultValueSql("(getdate())");

            entity.HasIndex(e => new { e.CheckedInAt, e.CheckedOutAt }, "IX_Shifts_CheckedInAt_CheckedOutAt");

            entity.HasIndex(e => e.StaffId, "IX_Shifts_StaffId");

            entity.HasIndex(e => new { e.StartAt, e.EndAt }, "IX_Shifts_StartAt_EndAt");


            entity.HasOne(d => d.CurrentLocation)
                .WithMany(p => p.ShiftsCurrentLocation)
                .HasForeignKey(d => d.CurrentLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shifts_CurrentLocation");

            entity.HasOne(d => d.StaffMember)
                .WithMany(p => p.Shifts)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shifts_Staff");

            entity.HasOne(d => d.StartingLocationNavigation)
                .WithMany(p => p.ShiftsStartingLocationNavigation)
                .HasForeignKey(d => d.StartingLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shifts_StartingLocation");
        }
    }
}
