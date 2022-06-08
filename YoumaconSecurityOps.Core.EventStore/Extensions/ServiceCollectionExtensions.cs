﻿namespace YoumaconSecurityOps.Core.EventStore.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });

    public static IServiceCollection AddEventStoreServices(this IServiceCollection services, string eventStoreConnectionString)
    {
        services.AddPooledDbContextFactory<EventStoreDbContext>(options =>
        {
            options.UseSqlServer(eventStoreConnectionString);
            options.UseLoggerFactory(_loggerFactory);
            options.EnableDetailedErrors();
            options.EnableServiceProviderCaching();
            options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IEventStoreRepository, EventStoreRepository>();

        return services;
    }

}