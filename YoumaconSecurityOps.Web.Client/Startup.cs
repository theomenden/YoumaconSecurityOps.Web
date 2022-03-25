namespace YoumaconSecurityOps.Web.Client;

public class Startup
{
    public Startup(IWebHostEnvironment webHost)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{webHost.EnvironmentName}.json", true);

        builder.AddEnvironmentVariables();

        Configuration = builder.Build();
        WebHostEnvironment = webHost;
        Assemblies = GetAssemblies();
    }

    public IConfigurationRoot Configuration { get; }
    
    public IWebHostEnvironment WebHostEnvironment { get; }

    public IDictionary<String, Assembly> Assemblies { get; set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        var initialScopes = Configuration.GetValue<String>("DownstreamApi:Scopes")?.Split(' ');

        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        var appSettings = new AppSettings
        {
            YoumaDbConnectionString = Configuration.GetConnectionString("YSecOpsDb"),
            EventStoreConnectionString = Configuration.GetConnectionString("YoumaEventStore"),
            YSecItAuthConnectionString = Configuration.GetConnectionString("YSecITSecurity")
        };
        
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog(dispose: true);
        });
        
        services
            .AddBlazorise(options =>
            {
                options.Immediate = true; // optional
            })
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();

        services.AddDataAccessServices(appSettings.YoumaDbConnectionString);

        services.AddEventStoreServices(appSettings.EventStoreConnectionString);

        services.AddMediatR(typeof(Program));

        services.AddAutoMappingServices();

        services.AddMediatrServices();

        services.AddFrontEndDataServices();
        
        services.AddResponseCompression(options =>
        {
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { MediaTypeNames.Application.Octet });
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "YoumaconSecurityOps.Api", Version = "v1" });
        });

        services.AddSingleton<SessionDetails>();
        services.AddScoped<CircuitHandler>(sp => new TrackingCircuitHandler(sp.GetRequiredService<SessionDetails>()));

        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
            .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
            .AddMicrosoftGraph(Configuration.GetSection("DownstreamApi"))
            .AddInMemoryTokenCaches();

        services.AddControllersWithViews()
            .AddMicrosoftIdentityUI();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });

        services.AddSignalR();
        
        services.AddServerSideBlazor()
            .AddMicrosoftIdentityConsentHandler();

        services.AddRazorPages();
    }
    
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseEnvironmentMiddleware(env);

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
    }

    private static IDictionary<String, Assembly> GetAssemblies()
    {
        var assemblies = new Dictionary<String, Assembly>(StringComparer.Ordinal)
        {
            {StartUp.Executing, typeof(Startup).GetTypeInfo().Assembly},
            {StartUp.Domain, typeof(BaseReader).GetTypeInfo().Assembly},
        };

        return assemblies;
    }
}

