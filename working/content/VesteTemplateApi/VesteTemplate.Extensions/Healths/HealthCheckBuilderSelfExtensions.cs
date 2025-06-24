using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using VesteTemplate.Extensions.Health.Customs;

namespace VesteTemplate.Extensions.Healths
{
    /// <summary>
    /// Extensão para verificar a saúde da própria aplicação.
    /// </summary>
    public static class HealthCheckBuilderSelfExtensions
    {
        public static IHealthChecksBuilder AddSelfCheck(this IHealthChecksBuilder builder, string name, HealthStatus? failureStatus = null, IEnumerable<string> tags = null)
        {
            builder.AddCheck<SelfHealthCheck>(name, failureStatus ?? HealthStatus.Degraded, tags);

            return builder;
        }
    }
}
