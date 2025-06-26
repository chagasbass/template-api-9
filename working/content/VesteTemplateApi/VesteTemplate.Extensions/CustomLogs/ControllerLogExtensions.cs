namespace VesteTemplate.Extensions.CustomLogs;
public static class ControllerLogExtensions
{
    public static IServiceCollection AddControllersWithValidationLogging(this IServiceCollection services)
    {
        services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var logServices = context.HttpContext.RequestServices.GetRequiredService<ILogServices>();

                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "Um ou mais erros de validação ocorreram.",
                            Instance = context.HttpContext.Request.Path
                        };

                        foreach (var erro in problemDetails.Errors)
                        {
                            erro.Value[0] = "O preenchimento do filtro é obrigatório.";
                        }

                        logServices.LogData
                                   .AddRequestType(context.HttpContext.Request.Method)
                                   .AddResponseStatusCode(StatusCodes.Status400BadRequest)
                                   .AddRequestBody(context.HttpContext.Items["RequestBody"]?.ToString())
                                   .AddResponseBody(JsonSerializer.Serialize(problemDetails));

                        logServices.WriteErrorLog();

                        return new BadRequestObjectResult(problemDetails);
                    };
                });

        return services;
    }
}
