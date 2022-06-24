
namespace YSecOps.Data.EfCore.Contexts
{
    public partial class YoumaconSecurityOpsContext : DbContext
    {
        public virtual DbSet<BannedList> BannedLists { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<NonStaffPerson> NonStaffPeople { get; set; }
        public virtual DbSet<Pronoun> Pronouns { get; set; }
        public virtual DbSet<RadioSchedule> RadioSchedules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoomSchedule> RoomSchedules { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffType> StaffTypes { get; set; }
        public virtual DbSet<StaffTypesRole> StaffTypesRoles { get; set; }
        public virtual DbSet<WatchList> WatchLists { get; set; }

        public YoumaconSecurityOpsContext(DbContextOptions<YoumaconSecurityOpsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BannedListConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new IncidentConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new NonStaffPersonConfiguration());
            modelBuilder.ApplyConfiguration(new PronounConfiguration());
            modelBuilder.ApplyConfiguration(new RadioScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoomScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftConfiguration());
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new StaffTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffTypesRoleConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListConfiguration());

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
