namespace YoumaconSecurityOps.Web.Client.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddFrontEndDataServices(this IServiceCollection services)
    {
        services
            .AddScoped<IEventReaderService, EventReaderService>()
            .AddScoped<IIncidentService, IncidentService>()
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<IRadioScheduleService, RadioScheduleService>()
            .AddScoped<IStaffService, StaffService>()
            .AddScoped<IShiftService, ShiftService>();

        return services;
    }
}
