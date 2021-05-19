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
    internal class RadioScheduleModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<RadioScheduleReader> entity)
        {
            entity.ToTable("RadioSchedule");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            entity.Property(e => e.LastStaffToHaveId).HasColumnName("LastStaffToHave_Id");

            entity.Property(e => e.LocationId).HasColumnName("Location_Id");

            entity.Property(e => e.SysEnd).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.SysStart).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.LastStaffToHave)
                .WithMany(p => p.RadioSchedule)
                .HasForeignKey(d => d.LastStaffToHaveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RadioSchedule_Staff");

            entity.HasOne(d => d.Location)
                .WithMany(p => p.RadioSchedule)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RadioSchedule_Location");
        }
    }
}
