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
        modelBuilder.ApplyConfiguration(new BannedListConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new IncidentConfiguration());
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new NonStaffPeopleConfiguration());
        modelBuilder.ApplyConfiguration(new PronounConfiguration());
        modelBuilder.ApplyConfiguration(new RadioScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new RoomScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new ShiftConfiguration());
        modelBuilder.ApplyConfiguration(new StaffConfiguration());
        modelBuilder.ApplyConfiguration(new StaffTypesRoleConfiguration());
        modelBuilder.ApplyConfiguration(new WatchListConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}