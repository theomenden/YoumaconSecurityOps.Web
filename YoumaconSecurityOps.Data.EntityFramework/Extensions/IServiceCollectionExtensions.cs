using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Repositories;
using YoumaconSecurityOps.Data.EntityFramework.Context;
using YoumaconSecurityOps.Data.EntityFramework.Repositories;

namespace YoumaconSecurityOps.Data.EntityFramework.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string youmaDbConnectionString)
        {
            services
                .AddDbContext<YoumaconSecurityDbContext>(options =>
                {
                    options.UseSqlServer(youmaDbConnectionString);
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                    options.EnableServiceProviderCaching();
                })
                .AddScoped<IContactAccessor, ContactRepository>()
                .AddScoped<IContactRepository, ContactRepository>()
                .AddScoped<IIncidentAccessor, IncidentRepository>()
                .AddScoped<IIncidentRepository, IncidentRepository>()
                .AddScoped<ILocationAccessor, LocationRepository>()
                .AddScoped<ILocationRepository, LocationRepository>()
                .AddScoped<IRadioScheduleAccessor, RadioScheduleRepository>()
                .AddScoped<IRadioScheduleRepository, RadioScheduleRepository>()
                .AddScoped<IShiftAccessor, ShiftRepository>()
                .AddScoped<IShiftRepository, ShiftRepository>()
                .AddScoped<IStaffAccessor, StaffRepository>()
                .AddScoped<IStaffRepository, StaffRepository>()
                .AddScoped<IStaffRoleAccessor, StaffRoleRepository>()
                .AddScoped<IStaffTypeAccessor, StaffTypeRepository>();

            return services;
        }
    }

}
