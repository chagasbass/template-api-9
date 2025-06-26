namespace VesteTemplate.Extensions.Health.Customs;

/// <summary>
/// Healthcheck customizado para verificar o consumo de memória da aplicação
/// </summary>
public class GarbageCollectorHealthcheck(IOptionsMonitor<GCInfoOptions> options) : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var allocatedMemory = GC.GetTotalMemory(forceFullCollection: false);
        GCInfoOptions.MaxMemory = MemoryConverterExtensions.ConvertMemorySize(options.CurrentValue.Threshold);
        GCInfoOptions.AllocatedMemory = MemoryConverterExtensions.ConvertMemorySize(allocatedMemory);

        var gcInfo = GC.GetGCMemoryInfo();
        GCInfoOptions.TotalAvailableMemory = MemoryConverterExtensions.ConvertMemorySize(gcInfo.TotalAvailableMemoryBytes);
        GCInfoOptions.SetOperationalSystem();

        if (allocatedMemory > options.CurrentValue.Threshold)
        {
            return Task.FromResult(new HealthCheckResult(
                                          HealthStatus.Degraded,
                                          description: HealthNames.MemoryDescriptionError));
        }

        return Task.FromResult(new HealthCheckResult(
                                      HealthStatus.Healthy,
                                      description: HealthNames.MemoryDescription));
    }
}
