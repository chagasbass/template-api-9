namespace VesteTemplate.Extensions.Logs.Entities;

/// <summary>
/// Representa o log estruturado.Pode ser alterado de acordo com a implementação
/// </summary>
public class LogData
{
    public DateTime Timestamp { get; private set; }
    public object RequestData { get; private set; }
    public string? RequestMethod { get; private set; }
    public string? RequestUri { get; private set; }
    public string? RequestQuery { get; private set; }
    public object ResponseData { get; private set; }
    public int ResponseStatusCode { get; private set; }
    public string? TraceId { get; private set; }
    public Exception Exception { get; private set; }
    public string? Mensagem { get; private set; } = "Requisição efetuada";

    public LogData()
    {
        Timestamp = DateTime.Now;
    }

    public LogData AddMessageInformation(string? mensagem)
    {
        Mensagem = mensagem;
        return this;
    }

    public LogData AddResponseStatusCode(int codigo)
    {
        ResponseStatusCode = codigo;
        return this;
    }

    public LogData AddRequestType(string metodo)
    {
        RequestMethod = metodo;
        return this;
    }

    public LogData AddRequestUrl(string? url)
    {
        RequestUri = url;
        return this;
    }

    public LogData AddTraceIdentifier(string? indentificador)
    {
        TraceId = indentificador;
        return this;
    }

    public LogData AddRequestBody(object requestData)
    {
        if (requestData is null)
            RequestData = "No Request Data";
        else
            RequestData = requestData;

        return this;
    }

    public LogData AddRequestBody(string requestData)
    {
        if (string.IsNullOrEmpty((string)requestData))
            RequestData = "No Request Data";
        else
            RequestData = requestData;

        return this;
    }

    public LogData AddRequestQuery(string requestQuery)
    {
        if (string.IsNullOrEmpty((string)requestQuery))
            RequestData = "No Request Query";
        else
            RequestQuery = requestQuery;

        return this;
    }

    public LogData AddResponseBody(object responseData)
    {
        ResponseData = responseData;

        return this;
    }

    public LogData AddException(Exception exception)
    {
        Exception = exception;
        return this;
    }

    public LogData ClearLogData()
    {
        Timestamp = DateTimeExtensions.GetGmtDateTime();
        RequestData = string.Empty;
        RequestMethod = string.Empty;
        RequestUri = string.Empty;
        ResponseData = string.Empty;
        ResponseStatusCode = 0;
        TraceId = string.Empty;

        return this;
    }

    public LogData ClearLogExceptionData()
    {
        Exception = default;

        return this;
    }

    private string FormatarObjetos(object data)
    {
        if (data is string str)
            return str;

        try
        {
            return System.Text.Json.JsonSerializer.Serialize(data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        }
        catch
        {
            return data.ToString();
        }
    }

    private string IndentarSaidaDeLog(string text, string indent)
    {
        if (string.IsNullOrEmpty(text))
            return indent + "N/A";

        return indent + text.Replace("\n", "\n" + indent);
    }

    public string DetalharLog()
    {
        var logMessage = new StringBuilder();
        logMessage.AppendLine("\n Detalhes da Requisição:");
        logMessage.AppendLine($"- Timestamp: {Timestamp:yyyy-MM-dd HH:mm:ss}");
        logMessage.AppendLine($"- Mensagem: {Mensagem ?? "N/A"}");

        if (!string.IsNullOrEmpty(RequestMethod) ||
            !string.IsNullOrEmpty(RequestUri) ||
            RequestData != null)
        {
            logMessage.AppendLine("\n[Request]");
            if (!string.IsNullOrEmpty(TraceId))
                logMessage.AppendLine($"  TraceId: {TraceId}");
            if (!string.IsNullOrEmpty(RequestMethod))
                logMessage.AppendLine($"  Método: {RequestMethod}");
            if (!string.IsNullOrEmpty(RequestUri))
                logMessage.AppendLine($"  URL: {RequestUri}");
            if (!string.IsNullOrEmpty(RequestQuery))
                logMessage.AppendLine($"  Query: {RequestQuery}");
            if (RequestData != null)
                logMessage.AppendLine($"  Body: {FormatarObjetos(RequestData)}");
        }


        if (ResponseStatusCode != 0 || ResponseData != null)
        {
            logMessage.AppendLine("\n[Response]");
            if (ResponseStatusCode != 0)
                logMessage.AppendLine($"  Status Code: {ResponseStatusCode}");
            if (ResponseData != null)
                logMessage.AppendLine($"  Body: {FormatarObjetos(ResponseData)}");
        }

        if (Exception != null)
        {
            logMessage.AppendLine("\n[Exception]");
            logMessage.AppendLine($"  Mensagem: {Exception.Message}");
            logMessage.AppendLine($"  StackTrace:\n{IndentarSaidaDeLog(Exception.StackTrace ?? "N/A", "    ")}");

            if (Exception.InnerException != null)
            {
                logMessage.AppendLine($"  InnerException:");
                logMessage.AppendLine($"    Mensagem: {Exception.InnerException.Message}");
                logMessage.AppendLine($"    StackTrace:\n{IndentarSaidaDeLog(Exception.InnerException.StackTrace ?? "N/A", "      ")}");
            }
        }

        return logMessage.ToString().Trim();
    }
}
