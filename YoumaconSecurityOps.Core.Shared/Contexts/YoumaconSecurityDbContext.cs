namespace YoumaconSecurityOps.Core.Shared.Contexts;

public partial class YoumaconSecurityDbContext : DbContext
{
    public virtual DbSet<BannedList> BannedLists { get; set; } = null!;
    public virtual DbSet<ContactReader> Contacts { get; set; } = null!;
    public virtual DbSet<IncidentReader> Incidents { get; set; } = null!;
    public virtual DbSet<LocationReader> Locations { get; set; } = null!;
    public virtual DbSet<NonStaffPeople> NonStaffPeoples { get; set; } = null!;
    public virtual DbSet<Pronoun> Pronouns { get; set; } = null!;
    public virtual DbSet<RadioScheduleReader> RadioSchedules { get; set; } = null!;
    public virtual DbSet<StaffRole> StaffRoles { get; set; } = null!;
    public virtual DbSet<RoomScheduleReader> RoomSchedules { get; set; } = null!;
    public virtual DbSet<ShiftReader> Shifts { get; set; } = null!;
    public virtual DbSet<StaffReader> StaffMembers { get; set; } = null!;
    public virtual DbSet<StaffType> StaffTypes { get; set; } = null!;
    public virtual DbSet<StaffTypesRole> StaffTypesRoles { get; set; } = null!;
    public virtual DbSet<WatchList> WatchLists { get; set; } = null!;

    public YoumaconSecurityDbContext(DbContextOptions<YoumaconSecurityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.BannedListConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ContactConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.IncidentConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.LocationConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.NonStaffPeopleConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RadioScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RoomScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ShiftConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.StaffConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.StaffTypesRoleConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.WatchListConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}