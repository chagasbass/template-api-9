using VesteTemplate.Shared.Enums;

namespace RestoqueMinimal.Extensions.Entities;

public record ApiProblemDetail
{
    public int Status { get; set; }
    public string? Title { get; set; }
    public string? Type { get; set; }
    public string? Instance { get; set; }
    public string? Detail { get; set; }
    public object InvalidParams { get; set; }

    public ApiProblemDetail(object invalidParams, string? detail, int status, string? title, string? instance = null)
    {
        InvalidParams = invalidParams;
        Detail = detail;
        Status = status;
        Title = title;
        Type = StatusCodeOperation.RetrieveStatusCode(status).Text;
        Instance = instance;
    }
}
