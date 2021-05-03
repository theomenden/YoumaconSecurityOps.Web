using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
