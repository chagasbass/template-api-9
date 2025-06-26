namespace VesteTemplate.Extensions.Healths;

public static class HealthCheckBuilderDatabaseExtensions
{
    public static IHealthChecksBuilder AddDatabaseCheck(this IHealthChecksBuilder builder, string name, HealthStatus? failureStatus = null, IEnumerable<string> tags = null)
    {
        builder.AddCheck<SqlServerHealthcheck>(name, failureStatus ?? HealthStatus.Degraded, tags);

        return builder;
    }
}
