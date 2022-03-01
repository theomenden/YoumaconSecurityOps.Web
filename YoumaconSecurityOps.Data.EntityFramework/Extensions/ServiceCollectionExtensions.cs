namespace YoumaconSecurityOps.Data.EntityFramework.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });

public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string youmaDbConnectionString)
    {
        services
            .AddPooledDbContextFactory<YoumaconSecurityDbContext>(options =>
            {
                options.UseLoggerFactory(_loggerFactory);
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
            .AddScoped<IStaffRoleMapRepository, StaffTypeRoleMapRepository>()
            .AddScoped<IStaffRoleAccessor, StaffRoleRepository>()
            .AddScoped<IStaffTypeAccessor, StaffTypeRepository>();

        return services;
    }
}