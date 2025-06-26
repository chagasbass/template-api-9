namespace VesteTemplate.Shared.Configurations;

public class RateLimitConfigurationOptions
{
    public const string? RateConfig = "RateLimitConfiguration";

    public string? LimitePermitido { get; set; }
    public int Janela { get; set; }
    public int QuantidadeDeEnfilaramento { get; set; }

    public RateLimitConfigurationOptions() { }
}
