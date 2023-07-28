using AspNet.Security.OAuth.Discord;
using Azure.Identity;
using Azure.Storage.Blobs;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.FluentValidation;
using Blazorise.Icons.FontAwesome;
using Blazorise.LoadingIndicator;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Serilog.Events;
using Serilog;
using YsecOps.UI.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

#region Bootstrap Logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .Enrich.FromLogContext()
    .Enrich.WithThreadName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessName()
    .Enrich.WithAssemblyVersion()
    .Enrich.WithAssemblyName()
    .Enrich.WithEnvironmentUserName()
    .Enrich.WithMemoryUsage()
    .Enrich.WithEventType()
    .WriteTo.Async(a =>
    {
        a.File("./logs/log-.txt", rollingInterval: RollingInterval.Day);
        a.Console();
    })
    .CreateBootstrapLogger();
#endregion

try
{
    var builder = WebApplication.CreateBuilder(args);

    var vaultUri = builder.Configuration["VaultUri"] ?? String.Empty;

    builder.Configuration.AddAzureKeyVault(new Uri(vaultUri), new DefaultAzureCredential());

    builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false)
        .UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithEventType()
        );

    var appInsightsConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];

    builder.Logging
        .ClearProviders()
        .AddApplicationInsights(
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

    builder.Services.AddBlazorise(options =>
        {
            options.Immediate = true;
            options.IconStyle = IconStyle.DuoTone;
            options.LicenseKey = builder.Configuration["blazorise-commercial"] ?? String.Empty;
        })
        .AddBootstrap5Providers()
        .AddBootstrap5Components()
        .AddFontAwesomeIcons()
        .AddLoadingIndicator()
        .AddBlazoriseFluentValidation();

    builder.Services.AddApplicationInsightsTelemetry(options => options.ConnectionString = appInsightsConnectionString);

    uilder.Services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
        })
        .AddDiscord(options =>
        {
            var discordClient = builder.Configuration["discord-clientId"] ?? String.Empty;
            var discordSecret = builder.Configuration["discord-secret"] ?? String.Empty;
            var discordKeys = new DiscordStrings(discordClient, discordSecret);

            options.ClientId = discordKeys.Id;
            options.ClientSecret = discordKeys.Secret;
            options.SaveTokens = true;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.Cookie.Name = builder.Configuration["Cookies:SharedCookieName"];
            options.Cookie.Path = builder.Configuration["Cookies:SharedCookiePath"];
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(5);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";

            options.SlidingExpiration = true;
        })
        .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"), cookieScheme: "microsoftCookies")
        .EnableTokenAcquisitionToCallDownstreamApi()
        .AddInMemoryTokenCaches();


    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.Name = builder.Configuration["Cookies:SharedCookieName"];
        options.Cookie.Path = builder.Configuration["Cookies:SharedCookiePath"];
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(5);

        options.LoginPath = "/Identity/Account/Login";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";

        options.SlidingExpiration = true;
    });


#if DEBUG
    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration["Cookies:PersistKeysDirectory"] ?? String.Empty))
        .SetApplicationName(builder.Configuration["Cookies:ApplicationName"] ?? String.Empty);
#endif

    if (builder.Environment.IsProduction())
    {
        var container = new BlobContainerClient(builder.Configuration["crowsagainststorage"],
            builder.Configuration["Application:KeyBase"]);
        const string blobName = "keys.xml";

        await container.CreateIfNotExistsAsync();

        var blobClient = container.GetBlobClient(blobName);

        var vaultKeyIdentifier = $"{vaultUri}{builder.Configuration["Application:KeyPathBase"]}";

        builder.Services.AddDataProtection()
            .PersistKeysToAzureBlobStorage(blobClient)
            .ProtectKeysWithAzureKeyVault(new Uri(vaultKeyIdentifier), new DefaultAzureCredential());
    }

    builder.Services.AddSingleton<ISessionDetails, SessionDetails>();

    builder.Services.AddScoped<CircuitHandler, TrackingCircuitHandler>(sp => new TrackingCircuitHandler(sp.GetRequiredService<ISessionDetails>()));

    builder.Services.AddResponseCaching();

    builder.Services.AddSignalR(options => options.MaximumReceiveMessageSize = 104_857_600)
        .AddJsonProtocol();

    builder.Services.AddBlazoredSessionStorage(options =>
    {
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        options.JsonSerializerOptions.WriteIndented = false;
    });

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();

    builder.Services.AddRazorPages();
    builder.Services.AddResponseCompression(options =>
    {
        options.MimeTypes = new[] { MediaTypeNames.Application.Octet };
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    })
        .Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest)
        .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.SmallestSize)
        .AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(365);
        }); ;
    builder.Services.AddControllers();
    builder.Services.AddSingleton<HttpClient>();
    builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    {
        options.TokenLifespan = TimeSpan.FromDays(2);
    });


    builder.Services.AddControllersWithViews()
        .AddMicrosoftIdentityUI();



    builder.Services.AddAuthorization(options =>
    {
        // By default, all incoming requests will be authorized according to the default policy
        options.FallbackPolicy = options.DefaultPolicy;
    });

    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor()
        .AddMicrosoftIdentityConsentHandler();
    builder.Services.AddSingleton<WeatherForecastService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync().ConfigureAwait(false);
}