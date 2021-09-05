using Microsoft.Extensions.DependencyInjection;
using YoumaconSecurityOps.Web.Client.Services;

namespace YoumaconSecurityOps.Web.Client.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFrontEndDataServices(this IServiceCollection services)
        {
            services
                    .AddScoped<IEventReaderService, EventReaderService>()
                    .AddScoped<ILocationService, LocationService>()
                    .AddScoped<IStaffService, StaffService>()
                    .AddScoped<IShiftService, ShiftService>()
                    .AddScoped<IIncidentService, IncidentService>();

            return services;
        }
    }
}
