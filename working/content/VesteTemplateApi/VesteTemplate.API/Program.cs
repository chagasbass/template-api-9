var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
#region configuração do log 
Log.Logger = LogExtensions.ConfigureStructuralLogWithSerilog(configuration);
builder.Logging.AddSerilog(Log.Logger);
#endregion

try
{
    Log.Information("Iniciando a aplicação");

    #region Injeção de Serviços
    builder.Services.AddControllersWithValidationLogging();
    builder.Services.AddEndpointsApiExplorer()
                    .SolveAppDependencyInjenctions()
                    .AddApplicationInsightsApiTelemetry(configuration)
                    .SolveStructuralAppDependencyInjection()
                    .AddOptionsPattern(configuration)
                    .AddGlobalCustomsMiddlewares(builder)
                    .AddCustomApiVersioning()
                    .AddSwaggerDocumentation(configuration)
                    .AddRequestResponseCompress()
                    .AddResponseRequestConfiguration()
                    .AddFilterToSystemLogs()
                    .AddAppHealthChecks()
                    .AddMapHealthChecksUi(configuration)
                    .AddApiAuthentication(configuration);

    #endregion

    var app = builder.Build();

    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    #region Inserção de Middlewares na pipeline do aspnetcore

    app.UseResponseCompression();
    app.UseExceptionHandler();
    app.UseMiddleware<SerilogRequestLoggerMiddleware>();

    app.UseSwagger();
    app.UseSwaggerUIMultipleVersions(provider);

    app.UseCors(x => x.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthenticationAndAuthorizationMiddlewares(configuration);
    app.UseHealthChecksMiddleware(configuration);
    app.MapControllers();

    #endregion

    await app.RunAsync();
}
catch (Exception ex)
{
    var log = $"Erro fatal na aplicação => {ex.Message}";
    Log.Fatal(log);
}
finally
{
    await Log.CloseAndFlushAsync();
}
