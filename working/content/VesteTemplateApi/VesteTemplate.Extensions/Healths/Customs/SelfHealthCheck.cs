namespace VesteTemplate.Extensions.Health.Customs;

/// <summary>
/// Healthcheck customizado para monitoramento da própria aplicação
/// </summary>
public class SelfHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new HealthCheckResult(
            HealthStatus.Healthy,
            description: HealthNames.SelfDescription));
    }
}

public class SqlServerHealthcheck : IHealthCheck
{
    private readonly BaseConfigurationOptions _options;

    public SqlServerHealthcheck(IOptionsMonitor<BaseConfigurationOptions> options)
    {
        _options = options.CurrentValue;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        using var conexao = new SqlConnection(_options.StringConexaoBancoDeDados);

        try
        {
            await conexao.OpenAsync();

            return new HealthCheckResult(
                HealthStatus.Healthy,
                description: HealthNames.SqlServerDescription);
        }
        catch (Exception ex)
        {
            return new HealthCheckResult(
                HealthStatus.Unhealthy,
                description: HealthNames.SqlServerDescriptionError);
        }
    }
}
