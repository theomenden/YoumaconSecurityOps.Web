using Ganss.Xss;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Azure;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithMachineName()
            .Enrich.WithProcessName()
            .Enrich.WithMemoryUsage()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithCorrelationId()
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
                .AddEnvironmentVariables()
                .AddAzureKeyVault(
                    new Uri(builder.Configuration["VaultUri"]),
                    new DefaultAzureCredential());
        })
        .UseDefaultServiceProvider(options => options.ValidateScopes = false)
        .UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

    var appInsightsConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];

    builder.Logging
        .ClearProviders().
        AddApplicationInsights(
            config => config.ConnectionString = appInsightsConnectionString,
            options =>
            {
                options.FlushOnDispose = true;
                options.IncludeScopes = true;
                options.TrackExceptionsAsExceptionTelemetry = true;
            }
            )
        .AddFilter<ApplicationInsightsLoggerProvider>(typeof(Program).FullName, LogLevel.Trace)
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
    builder.Services.AddApplicationInsightsTelemetry(options => options.ConnectionString = appInsightsConnectionString);
    services.AddSingleton<SessionDetails>();
    services.AddScoped<CircuitHandler>(sp => new TrackingCircuitHandler(sp.GetRequiredService<SessionDetails>()));

    services.RegisterAzureDataServices(configurationManager);

    services.AddRazorPages();

    services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(x =>
    {
        // Configure sanitizer rules as needed here.
        // For now, just use default rules + allow class attributes
        var sanitizer = new HtmlSanitizer();
        sanitizer.AllowedAttributes.Add("class");
        return sanitizer;
    });

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