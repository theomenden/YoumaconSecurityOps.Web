Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithMachineName()
            .Enrich.WithProcessName()
            .Enrich.WithMemoryUsage()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithEventType()
            .WriteTo.Async(a =>
            {
                a.File("./logs/log-.txt", rollingInterval: RollingInterval.Day);
                a.Console();
            })
            .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder();

    builder.Host
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
        })
        .UseDefaultServiceProvider(options => options.ValidateScopes = false)
        .UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithEventType());

    builder.Logging
        .ClearProviders()
        .AddSerilog(dispose: true);

    #region Configure Application Services
    var configurationManager = builder.Configuration;
    var webHostEnvironment = builder.Environment;
    var services = builder.Services;

    JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

    var appSettings = new AppSettings
    {
        YoumaDbConnectionString = configurationManager.GetConnectionString("YSecOpsDb"),
        EventStoreConnectionString = configurationManager.GetConnectionString("YoumaEventStore"),
        YSecItAuthConnectionString = configurationManager.GetConnectionString("YSecITSecurity")
    };

    services
        .AddBlazorise(options =>
        {
            options.Immediate = true; // optional
            options.ShowNumericStepButtons = true;
        })
        .AddBootstrap5Providers()
        .AddBootstrap5Components()
        .AddFontAwesomeIcons();

    services.AddApplicationRegistrations(appSettings);

    services.AddTransient<IUrlHasher, UrlHasher>();
    services.AddSingleton<SessionDetails>();
    services.AddScoped<CircuitHandler>(sp => new TrackingCircuitHandler(sp.GetRequiredService<SessionDetails>()));

    services.RegisterAzureDataServices(configurationManager);

    services.AddRazorPages();
    #endregion
    #region Application Configuration
    await using var app = builder.Build();

    app.UseEnvironmentMiddleware(app.Environment);

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging(options => options.EnrichDiagnosticContext = RequestLoggingConfigurer.EnrichFromRequest);

    app.UseStaticFiles();

    app.UseRouting();

    app.UseApiExceptionHandler(options =>
    {
        options.AddResponseDetails = OptionsDelegates.UpdateApiErrorResponse;
        options.DetermineLogLevel = OptionsDelegates.DetermineLogLevel;
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
    });
    #endregion

    Log.Information("{ApplicationName} Starting...", nameof(YoumaconSecurityOps));
    await app.RunAsync();
    Log.Information("{ApplicationName} Stopping...", nameof(YoumaconSecurityOps));
}
catch (Exception ex)
{
    Log.Fatal("{ApplicationName} crashed before the application could start. {@Ex}", nameof(YoumaconSecurityOps), ex);
}
finally
{
    Log.CloseAndFlush();
}