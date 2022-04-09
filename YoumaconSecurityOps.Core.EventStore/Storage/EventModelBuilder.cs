using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YoumaconSecurityOps.Core.EventStore.Storage;

internal class EventModelBuilder
{
    public static void BuildModel(EntityTypeBuilder<EventReader> entity)
    {
        entity.ToTable("Events", "Diagnostics");

        entity.HasKey(e => e.Id)
            .HasName("PK_Events_Id")
            .IsClustered(false);

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        entity.Property(e => e.CreatedAt)
            .ValueGeneratedOnAdd()
            .IsRequired();

        entity.Property(e => e.Data)
            .IsRequired();

        entity.Property(e => e.MinorVersion)
            .HasColumnName("Minor_Version")
            .IsRequired();

        entity.Property(e => e.MajorVersion)
            .HasColumnName("Major_Version")
            .IsRequired();

        entity.Property(e => e.Name)
            .IsRequired();

        entity.Property(e => e.Aggregate)
            .HasColumnName("Aggregate")
            .IsRequired();

        entity.Property(e => e.AggregateId)
            .HasColumnName("AggregateId")
            .IsRequired();

        entity.HasIndex(e => new { e.MajorVersion, e.MinorVersion }, "IX_Events_MajorMinor")
            .IncludeProperties(e => new { e.Name, e.Aggregate, e.Data });

        entity.HasIndex(e => e.AggregateId, "IX_Events_AggregateId")
            .IncludeProperties(e => new { e.Name, e.Aggregate, e.Data });
    }
}
