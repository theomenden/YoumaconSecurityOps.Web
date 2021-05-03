using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Data.EntityFramework.ModelBuilders;

namespace YoumaconSecurityOps.Data.EntityFramework
{
    public class YoumaconSecurityDbContext : DbContext
    {
        public YoumaconSecurityDbContext(DbContextOptions<YoumaconSecurityDbContext> options)
        : base(options)
        {

        }

        public DbSet<ContactReader> Contacts { get; set; }

        public DbSet<LocationReader> Locations { get; set; }

        public DbSet<StaffReader> StaffMembers { get; set; }

        public DbSet<StaffRole> StaffRoles { get; set; }

        public DbSet<StaffType> StaffTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ContactModelBuilder.BuildModel(modelBuilder.Entity<ContactReader>());

            LocationModelBuilder.BuildModel(modelBuilder.Entity<LocationReader>());

            StaffModelBuilder.BuildModel(modelBuilder.Entity<StaffReader>());
        }
    }
}
