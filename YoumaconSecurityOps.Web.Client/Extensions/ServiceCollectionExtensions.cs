namespace YoumaconSecurityOps.Web.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFrontEndDataServices(this IServiceCollection services)
    {
        services
            .AddScoped<IContactService, ContactReaderService>()
            .AddScoped<IEventReaderService, EventReaderService>()
            .AddScoped<IIncidentService, IncidentService>()
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<IRadioScheduleService, RadioScheduleService>()
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IStaffService, StaffService>()
            .AddScoped<IShiftService, ShiftService>();

        return services;
    }
}
