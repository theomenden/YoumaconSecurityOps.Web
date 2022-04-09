Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithMachineName()
            .Enrich.WithProcessName()
            .Enrich.WithEnvironmentUserName()
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
            var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
            config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
        })
        .UseDefaultServiceProvider(options => options.ValidateScopes = false)
        .UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services));

    var startup = new Startup(builder.Environment);

    Log.Information("{applicationName} started", nameof(YoumaconSecurityOps));

    startup.ConfigureServices(builder.Services);

    var app = builder.Build();

    startup.Configure(app, app.Environment);
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal("{applicationName} crashed before the application could start. {@ex}", nameof(YoumaconSecurityOps), ex);
}

finally
{
    Log.CloseAndFlush();
}
