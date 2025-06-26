namespace VesteTemplate.Extensions.Middlewares;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddGlobalCustomsMiddlewares(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<ExceptionHandlerOptions>(options =>
        {
            options.ExceptionHandler = async context =>
            {
                var exceptionHandler = context.RequestServices.GetRequiredService<IExceptionHandler>();
                await exceptionHandler.TryHandleAsync(context, context.Features.Get<IExceptionHandlerFeature>()?.Error, context.RequestAborted);
            };
        });

        services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();

        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    context.ProblemDetails.Extensions.Remove("exception");
                }
            };
        });

        services.AddTransient<UnauthorizedTokenMiddleware>();

        return services;
    }
}
