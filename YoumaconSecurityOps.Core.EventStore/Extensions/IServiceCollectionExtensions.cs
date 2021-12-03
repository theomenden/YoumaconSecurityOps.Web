using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using YoumaconSecurityOps.Core.EventStore.Storage;

namespace YoumaconSecurityOps.Core.EventStore.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStoreServices(this IServiceCollection services, string eventStoreConnectionString)
        {
            services.AddDbContext<EventStoreDbContext>(options =>
            {
                options.UseSqlServer(eventStoreConnectionString);
                options.EnableDetailedErrors();
                options.EnableServiceProviderCaching();
                options.EnableSensitiveDataLogging();
            });

            services.AddMediatR(typeof(IServiceCollectionExtensions).Assembly);

            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

            return services;
        }
    }
}
