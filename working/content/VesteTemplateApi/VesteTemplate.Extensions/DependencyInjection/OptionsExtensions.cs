using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VesteTemplate.Shared.Configurations;

namespace VesteTemplate.Extensions.DependencyInjection;

public static class OptionsExtensions
{
    public static IServiceCollection AddOptionsPattern(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BaseConfigurationOptions>(configuration.GetSection(BaseConfigurationOptions.BaseConfig));
        services.Configure<HealthchecksConfigurationOptions>(configuration.GetSection(HealthchecksConfigurationOptions.BaseConfig));
        services.Configure<ResilienceConfigurationOptions>(configuration.GetSection(ResilienceConfigurationOptions.ResilienciaConfig));
        services.Configure<EmailConfigurationOptions>(configuration.GetSection(EmailConfigurationOptions.EmailConfig));

        return services;
    }
}
