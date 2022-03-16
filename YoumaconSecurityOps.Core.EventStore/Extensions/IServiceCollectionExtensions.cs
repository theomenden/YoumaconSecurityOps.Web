namespace YoumaconSecurityOps.Core.EventStore.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddEventStoreServices(this IServiceCollection services, string eventStoreConnectionString)
    {
        services.AddPooledDbContextFactory<EventStoreDbContext>(options =>
        {
            options.UseSqlServer(eventStoreConnectionString);
            options.EnableDetailedErrors();
            options.EnableServiceProviderCaching();
            options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IEventStoreRepository, EventStoreRepository>();

        return services;
    }
}