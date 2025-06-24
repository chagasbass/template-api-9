var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
#region configuring logs
Log.Logger = LogExtensions.ConfigureStructuralLogWithSerilog(configuration);
builder.Logging.AddSerilog(Log.Logger);
#endregion

try
{
    Log.Information("Iniciando a aplica��o");
    builder.UseStartup<Startup>();
}
catch (Exception ex)
{
    Log.Fatal($"Erro fatal na aplica��o => {ex.Message}");
}
finally
{
    Log.CloseAndFlush();
}
