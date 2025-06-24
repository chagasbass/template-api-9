using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using VesteTemplate.Extensions.Health.Entities;
using VesteTemplate.Shared.Extensions;

namespace VesteTemplate.Extensions.Health.Customs
{
    /// <summary>
    /// Healthcheck customizado para verificar o consumo de memória da aplicação
    /// </summary>
    public class GarbageCollectorHealthcheck : IHealthCheck
    {
        private readonly GCInfoOptions _options;

        public GarbageCollectorHealthcheck(IOptionsMonitor<GCInfoOptions> options)
        {
            _options = options.CurrentValue;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var allocatedMemory = GC.GetTotalMemory(forceFullCollection: false);
            GCInfoOptions.MaxMemory = MemoryConverterExtensions.ConvertMemorySize(_options.Threshold);
            GCInfoOptions.AllocatedMemory = MemoryConverterExtensions.ConvertMemorySize(allocatedMemory);

            var gcInfo = GC.GetGCMemoryInfo();
            GCInfoOptions.TotalAvailableMemory = MemoryConverterExtensions.ConvertMemorySize(gcInfo.TotalAvailableMemoryBytes);
            GCInfoOptions.SetOperationalSystem();

            if (allocatedMemory > _options.Threshold)
            {
                return new HealthCheckResult(
                                              HealthStatus.Degraded,
                                              description: HealthNames.MemoryDescription);
            }

            return new HealthCheckResult(
                                          HealthStatus.Healthy,
                                          description: HealthNames.MemoryDescription);
        }
    }
}
