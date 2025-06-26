namespace VesteTemplate.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection SolveStructuralAppDependencyInjection(this IServiceCollection services)
    {
        services.AddStructuraLog();
        services.AddNotificationControl();

        return services;
    }
}
