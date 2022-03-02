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
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        var appSettings = new AppSettings
        {
            YoumaDbConnectionString = Configuration.GetConnectionString("YSecOpsDb"),
            EventStoreConnectionString = Configuration.GetConnectionString("YoumaEventStore"),
            YSecItAuthConnectionString = Configuration.GetConnectionString("YSecITSecurity")
        };

        services.AddLogging(builder =>
        {
            builder.AddSerilog(dispose: true);
        });

        services.AddAuthServices(appSettings);

        services
            .AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true; // optional
            })
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();

        services.AddRazorPages();

        services.AddMediatR(typeof(Program));

        services.AddDataAccessServices(appSettings.YoumaDbConnectionString);

        services.AddAutoMappingServices();

        services.AddMediatrServices();

        services.AddEventStoreServices(appSettings.EventStoreConnectionString);

        services.AddFrontEndDataServices();

        services.AddFluentEmailServices(Configuration);

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

        services.AddSignalR();

        services.AddServerSideBlazor();

        services.AddIndexedDB(dbStore =>
        {
            dbStore.DbName = nameof(YoumaconSecurityOps);
            dbStore.Version = 1;

            dbStore.Stores.Add(new StoreSchema
            {
                Name = "YSecRoles",
                PrimaryKey = new(){Auto = false, Name = "roleId", KeyPath = "id"},
                Indexes = new()
                {
                    new()
                    {
                        Auto = false,
                        Name = "Name",
                        KeyPath = nameof(StaffRole.Name),
                    }
                }
            });

            dbStore.Stores.Add(new StoreSchema
            {
                Name = "YSecStaffTypes",
                PrimaryKey = new() { Auto = false, Name = "staffTypeId", KeyPath = "id" },
                Indexes = new ()
                {
                    new()
                    {
                        Auto = false,
                        Name = "Title",
                        KeyPath = nameof(StaffType.Title),
                    }
                }
            });
        });

        services.RegisterBuiltCaches();

        services.RegisterBuiltStreamingCaches();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseMiddleware(typeof(ExceptionLogger));

        app.UseMiddleware(typeof(ExceptionHandler));

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}

