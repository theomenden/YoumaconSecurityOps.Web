using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.EventStore.Events;

namespace YoumaconSecurityOps.Core.EventStore.Storage
{
    public class EventStoreDbContext: DbContext
    {
        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options)
            :base(options)
        {

        }

        public DbSet<EventReader> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EventModelBuilder.BuildModel(modelBuilder.Entity<EventReader>());
        }
    }
}
