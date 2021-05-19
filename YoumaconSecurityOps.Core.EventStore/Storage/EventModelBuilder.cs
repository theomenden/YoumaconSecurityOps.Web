using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoumaconSecurityOps.Core.EventStore.Events;

namespace YoumaconSecurityOps.Core.EventStore.Storage
{
    internal class EventModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<EventReader> entity)
        {
            entity.ToTable("Events");

            entity.HasKey(e => new {e.MinorVersion, e.Id})
                .HasName("PK_Events_Id");

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

            entity.HasIndex(e => new { e.MajorVersion, e.MinorVersion }, "IX_Events_MajorMinor");
        }
    }
}
