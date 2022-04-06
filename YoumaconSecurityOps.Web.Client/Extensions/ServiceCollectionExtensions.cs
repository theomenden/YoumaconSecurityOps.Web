using Nosthy.Blazor.DexieWrapper.JsModule;
using YoumaconSecurityOps.Web.Client.IndexedDb.Context;
using YoumaconSecurityOps.Web.Client.IndexedDb.Repositories;

namespace YoumaconSecurityOps.Web.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFrontEndDataServices(this IServiceCollection services)
    {
        services.AddSingleton<YSecServiceOptions>();

        services
            .AddScoped<IContactService, ContactService>()
            .AddScoped<IEventReaderService, EventReaderService>()
            .AddScoped<IIncidentService, IncidentService>()
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<IRadioScheduleService, RadioScheduleService>()
            .AddScoped<IRoomService, RoomService>()
            .AddScoped<IStaffService, StaffService>()
            .AddScoped<IShiftService, ShiftService>();

        return services;
    }

    public static IServiceCollection AddIndexedDbServices(this IServiceCollection services)
    {
        services
            .AddScoped<IModuleFactory, EsModuleFactory>()
            .AddScoped<YsecIndexedDbContext>()
            .AddScoped<PronounsIndexedDbRepository>()
            .AddScoped<LocationsIndexedDbRepository>()
            .AddScoped<StaffRolesIndexedDbRepository>()
            .AddScoped<StaffTypesIndexedDbRepository>();
        
        return services;
    }
}
