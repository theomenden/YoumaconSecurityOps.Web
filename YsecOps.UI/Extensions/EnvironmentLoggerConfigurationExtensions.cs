using Serilog;
using Serilog.Configuration;

namespace YsecOps.UI.Extensions
{
    public static class EnvironmentLoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithEventType(this LoggerEnrichmentConfiguration enrichmentConfiguration) =>
            enrichmentConfiguration is null
                ? throw new ArgumentNullException(nameof(enrichmentConfiguration))
                : enrichmentConfiguration.With<EventTypeEnricher>();
    }
}
