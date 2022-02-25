namespace YoumaconSecurityOps.Core.EventStore.Storage;

public class EventStoreDbContext : DbContext
{
    public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options)
        : base(options)
    { }

    public DbSet<EventReader> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EventModelBuilder.BuildModel(modelBuilder.Entity<EventReader>());
    }
}

