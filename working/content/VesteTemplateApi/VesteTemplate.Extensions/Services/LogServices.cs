namespace VesteTemplate.Extensions.Logs.Services;

public class LogServices : ILogServices
{
    public LogData LogData { get; set; }
    BaseConfigurationOptions Options { get; set; }

    private readonly Serilog.ILogger _logger = Log.ForContext<LogServices>();

    public LogServices(IOptionsMonitor<BaseConfigurationOptions> options)
    {
        LogData = new LogData();
        Options = options.CurrentValue;
    }

    public void WriteLog()
    {
        if (Options.HabilitarMensagensDeLog)
        {
            var logMessage = LogData.DetalharLog();

            _logger.Information(logMessage);
        }

        LogData.ClearLogData();
    }

    public void WriteLogWhenRaiseExceptions()
    {
        var logMessage = LogData.DetalharLog();

        Log.Error(logMessage);

        LogData.ClearLogExceptionData();
    }

    public void CreateStructuredLog(LogData LogData) => LogData = LogData;

    public void WriteMessage(string? mensagem)
    {
        if (Options.HabilitarMensagensDeLog)
        {
            _logger.Information($"{mensagem}");
        }
    }

    public void WriteErrorLog()
    {
        var logMessage = new StringBuilder("Log de requisição:");
        logMessage.Append($" [Timestamp]: {LogData.Timestamp:yyyy-MM-dd HH:mm:ss},");
        logMessage.Append($" [Mensagem]: {LogData.Mensagem},");

        if (!string.IsNullOrEmpty(LogData.RequestMethod))
            logMessage.Append($" [RequestMethod]: {LogData.RequestMethod},");

        if (!string.IsNullOrEmpty(LogData.RequestUri))
            logMessage.Append($" [RequestUri]: {LogData.RequestUri},");

        if (!string.IsNullOrEmpty(LogData.RequestQuery))
            logMessage.Append($" [RequestQuery]: {LogData.RequestQuery},");

        if (!string.IsNullOrEmpty(LogData.TraceId))
            logMessage.Append($" [TraceId]: {LogData.TraceId},");

        if (LogData.RequestData != null)
            logMessage.Append($" [RequestData]: {{{LogData.RequestData}}},");

        if (LogData.ResponseStatusCode != 0)
            logMessage.Append($" [ResponseStatusCode]: {LogData.ResponseStatusCode},");

        if (LogData.ResponseData != null)
            logMessage.Append($" [ResponseData]: {{{LogData.ResponseData}}},");

        if (logMessage[logMessage.Length - 1] == ',')
            logMessage.Length--;

        _logger.Error(logMessage.ToString());

        LogData.ClearLogData();
    }

    public void WriteStaticMessage(string mensagem)
    {
        _logger.Information(mensagem);
    }

    public void WriteLogFromResiliences()
    {
        _logger.Information("[LogRequisição]:{mensagem} [RequestUri]:{RequestUri} [ResponseStatusCode]:{ResponseStatusCode}  [RequestData]:{@RequestData}  [RequestQuery]:{RequestQuery} [ResponseData]:{@ResponseData}" +
             "[Timestamp]:{Timestamp}", LogData.Mensagem, LogData.RequestUri, LogData.ResponseStatusCode, LogData.RequestData, LogData.RequestQuery, LogData.ResponseData, LogData.Timestamp);

        LogData.ClearLogData();
    }
}
