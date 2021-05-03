using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YoumaconSecurityOps.Core.EventStore.Storage;

namespace YoumaconSecurityOps.Core.EventStore.Tests.StorageTests
{
    public class YSecTestEventStoreContext: EventStoreDbContext
    {
        public YSecTestEventStoreContext() 
            : base(Options())
        {
        }

        private static DbContextOptions<EventStoreDbContext> Options()
        {
            return new DbContextOptionsBuilder<EventStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
        }

        public override void Dispose()
        {
            Database.EnsureDeleted();
        }
    }
}
