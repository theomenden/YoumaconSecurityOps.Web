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

            entity.HasKey(e => new {e.MinorVersion, e.AggregateId});

            entity.Property(e => e.Id)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.Data)
                .IsRequired();

            entity.Property(e => e.AggregateId)
                .IsRequired();

            entity.Property(e => e.MinorVersion)
                .IsRequired();

            entity.Property(e => e.MajorVersion)
                .IsRequired();

            entity.Property(e => e.Name)
                .IsRequired();
        }
    }
}
