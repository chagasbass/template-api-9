namespace VesteTemplate.Extensions.Resiliences;

public static class ResilienceExtensions
{
    public static IServiceCollection AddWorkerResiliencesPatterns(this IServiceCollection services, IConfiguration configuration)
    {
        var quantidadeDeRetentativas = Int32.Parse(configuration["ResilienceConfiguration:QuantidadeDeRetentativas"]);
        var nomeCliente = configuration["ResilienceConfiguration:NomeCliente"];

        services.AddHttpClient(nomeCliente)
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(ResiliencePolicies.GetApiRetryPolicy(quantidadeDeRetentativas));

        return services;
    }
}
