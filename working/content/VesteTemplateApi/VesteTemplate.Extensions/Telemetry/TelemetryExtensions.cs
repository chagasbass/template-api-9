
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VesteTemplate.Extensions.Telemetry
{
    public static class TelemetryExtensions
    {
        public static IServiceCollection AddApplicationInsightsApiTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ApplicationInsights");

            var options = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
            {
                EnableAdaptiveSampling = true,
                ConnectionString = connectionString,
                EnableHeartbeat = false
            };

            services.AddApplicationInsightsTelemetry(options);

            return services;
        }

        public static IServiceCollection AddApplicationInsightsWorkerTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ApplicationInsights");

            services.AddApplicationInsightsTelemetryWorkerService(new Microsoft.ApplicationInsights.WorkerService.ApplicationInsightsServiceOptions
            {
                ConnectionString = connectionString
            });

            return services;
        }
    }
}
