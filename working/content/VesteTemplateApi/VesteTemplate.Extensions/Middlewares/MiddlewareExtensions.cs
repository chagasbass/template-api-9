using Microsoft.Extensions.DependencyInjection;

namespace VesteTemplate.Extensions.Middlewares;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddGlobalCustomsMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandlerMiddleware>();
        services.AddTransient<UnauthorizedTokenMiddleware>();

        return services;
    }
}
