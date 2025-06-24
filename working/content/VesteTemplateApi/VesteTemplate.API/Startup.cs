namespace VesteTemplate.Api;

public class Startup : IBaseStartup
{
    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer()
                .SolveAppDependencyInjenctions()
                .AddApplicationInsightsApiTelemetry(Configuration)
                .SolveStructuralAppDependencyInjection()
                .AddOptionsPattern(Configuration)
                .AddGlobalCustomsMiddlewares()
                .AddCustomApiVersioning()
                .AddSwaggerDocumentation(Configuration)
                .AddRequestResponseCompress()
                .AddResponseRequestConfiguration()
                .AddFilterToSystemLogs()
                .AddAppHealthChecks()
                .AddMapHealthChecksUi(Configuration)
                .AddApiAuthentication(Configuration);
    }

    public void Configure(WebApplication app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        app.UseResponseCompression();

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        app.UseMiddleware<SerilogRequestLoggerMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUIMultipleVersions(provider);

        app.UseCors(x => x.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseMiddleware<UnauthorizedTokenMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHealthChecksMiddleware(Configuration);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
