using YoumaconSecurityOps.Data.EntityFramework.Context.Configurations;

namespace YoumaconSecurityOps.Data.EntityFramework.Context
{
    public partial class YoumaconSecurityDbContext : DbContext
    {
        public YoumaconSecurityDbContext(DbContextOptions<YoumaconSecurityDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<ContactReader> Contacts { get; set; }

        public virtual DbSet<IncidentReader> Incidents { get; set; }

        public virtual DbSet<LocationReader> Locations { get; set; }

        public virtual DbSet<RadioScheduleReader> RadioSchedules { get; set; }

        public virtual DbSet<RoomScheduleReader> RoomSchedules { get; set; }

        public virtual DbSet<ShiftReader> Shifts { get; set; }

        public virtual DbSet<StaffReader> StaffMembers { get; set; }

        public virtual DbSet<StaffRole> StaffRoles { get; set; }

        public virtual DbSet<StaffType> StaffTypes { get; set; }

        public virtual DbSet<StaffTypesRoles> StaffTypesRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new BannedListConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new IncidentConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new RadioScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new RoomScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftConfiguration());
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new StaffTypesRoleConfiguration());
            modelBuilder.ApplyConfiguration(new StaffRoleConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListConfiguration());
            OnModelCreatingPartial(modelBuilder);
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
