using System;
using Microsoft.EntityFrameworkCore;
using YoumaconSecurityOps.Data.EntityFramework.Context;

namespace YoumaconSecurityOps.Data.EntityFramework.Tests
{
    public sealed class YoumaconTestDbContext: YoumaconSecurityDbContext
    {
        public YoumaconTestDbContext() 
            : base(Options())
        {
        }
        private static DbContextOptions<YoumaconSecurityDbContext> Options()
        {
            return new DbContextOptionsBuilder<YoumaconSecurityDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
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
