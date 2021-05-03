using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.EventStore.Storage;

namespace YoumaconSecurityOps.Core.EventStore.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStoreServices(this IServiceCollection services, string eventStoreConnectionString)
        {
            services.AddDbContext<EventStoreDbContext>(options => options.UseSqlServer(eventStoreConnectionString));

            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

            return services;
        }
    }
}
