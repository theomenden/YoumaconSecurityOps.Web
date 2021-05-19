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
    internal class RoomScheduleModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<RoomScheduleReader> entity)
        {
            entity.ToTable("RoomSchedule");

            entity.HasIndex(e => e.LocationId, "IX_RoomSchedule_Location");

            entity.Property(e => e.Id)
                  .HasDefaultValueSql("(newsequentialid())");

            entity.Property(e => e.LastStaffInRoomId)
                  .HasColumnName("LastStaffInRoom_Id");

            entity.Property(e => e.LocationId)
                  .HasColumnName("Location_Id");

            entity.Property(e => e.NumberOfKeys)
                  .HasDefaultValueSql("((1))");

            entity.Property(e => e.RoomNumber)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.HasOne(d => d.LastStaffInRoom)
                  .WithMany(p => p.RoomSchedule)
                  .HasForeignKey(d => d.LastStaffInRoomId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_RoomSchedule_Staff");
        }
    }
}
