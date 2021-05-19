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
    internal class IncidentModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<IncidentReader> entity)
        {
            entity.HasIndex(e => e.RecordedById, "IX_Incidents_RecordedBy");

            entity.HasIndex(e => e.ReportedById, "IX_Incidents_ReportedBy");

            entity.HasIndex(e => new { e.RecordedOn, e.Severity }, "IX_Incidents_Severity");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(e => e.LocationId).HasColumnName("Location_Id");

            entity.Property(e => e.RecordedById).HasColumnName("RecordedBy_Id");

            entity.Property(e => e.ReportedById).HasColumnName("ReportedBy_Id");

            entity.Property(e => e.ShiftId).HasColumnName("Shift_Id");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Location)
                .WithMany(p => p.Incidents)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Incidents_Locations");

            entity.HasOne(d => d.RecordedBy)
                .WithMany(p => p.IncidentsRecordedBy)
                .HasForeignKey(d => d.RecordedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Incidents_RecordedBy");

            entity.HasOne(d => d.ReportedBy)
                .WithMany(p => p.IncidentsReportedBy)
                .HasForeignKey(d => d.ReportedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Incidents_ReportedBy");

            entity.HasOne(d => d.Shift)
                .WithMany(p => p.Incidents)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Incidents_Shifts");
        }
    }
}
