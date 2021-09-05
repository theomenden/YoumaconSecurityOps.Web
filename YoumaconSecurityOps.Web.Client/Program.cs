using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace YoumaconSecurityOps.Web.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
             Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithMachineName()
                .Enrich.WithProcessName()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.Async(a =>
                {
                    a.File($"./logs/log-.txt", rollingInterval: RollingInterval.Day);
                    a.Console();
                })
                .CreateBootstrapLogger();

             try
             {
                 Log.Information($"{nameof(YoumaconSecurityOps)} started");
                 CreateHostBuilder(args).Build().Run();
             }
             catch (Exception ex)
             {
                 Log.Fatal(ex, $"{nameof(Main)} crashed before the application could start", ex.InnerException);
             }

             finally
             {
                 Log.CloseAndFlush();
             }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .Enrich.WithThreadId()
                    .Enrich.WithProcessName()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentUserName())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
