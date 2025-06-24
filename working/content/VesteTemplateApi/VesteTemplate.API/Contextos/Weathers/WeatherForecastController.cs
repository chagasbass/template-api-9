using System.Net.Mime;
using VesteTemplateApi.Domain.Entities;

namespace VesteTemplateApi.Contextos.Weathers;

[ApiController]
[ApiVersion("1.0")]
[ConditionalAuthorize]
[Route("v{version:apiVersion}/weathers")]
[ApiExplorerSettings(GroupName = "Weathers")]
public class WeatherForecastController(ILogServices logServices, INotificationServices notificationServices) : ApiBaseController(logServices, notificationServices)
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    /// <summary>
    /// Recupera uma lista de objetos de Weather Forecasts
    /// </summary>
    /// <response code="200">Retorna quando a pesquisa encontra resultados</response>
    /// <response code="400">Retorna quando os filtros não são preenchidos</response>
    /// <response code="404">Retorna quando a pesquisa não encontra resultados</response>
    /// <response code="500">Retorna quando algum problema inesperado acontece na chamada</response>
    /// <returns>Retorna uma lista contendo os weathers que atendam a pesquisa por filtro</returns>
    [HttpGet()]
    [MapToApiVersion("1.0")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CommandResult>> GetWeathersAsync()
    {
        var weathers = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        var commandResult = new CommandResult(weathers, true, "verifique os dados");

        return FormatApiResponse(commandResult);
    }
}


[ApiController]
[ApiVersion("2.0")]
[ConditionalAuthorize]
[Route("v{version:apiVersion}/weathers")]
[ApiExplorerSettings(GroupName = "Weathers")]
public class WeatherForecastControllerV2(ILogServices logServices, INotificationServices notificationServices) : ApiBaseController(logServices, notificationServices)
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    /// <summary>
    /// Recupera uma lista de objetos de Weather Forecasts
    /// </summary>
    /// <param name="sumary">Filtro que representa o parâmetro da consulta
    /// </param>
    /// <remarks>
    /// 
    /// ** O Filtro sumary é obrigatório e  deve ser informado na busca.
    /// Exemplo request:
    /// 
    ///     GET /weathers
    ///     {
    ///        "summary":"Chill"
    ///     }
    /// </remarks>
    /// <response code="200">Retorna quando a pesquisa encontra resultados</response>
    /// <response code="400">Retorna quando os filtros não são preenchidos</response>
    /// <response code="404">Retorna quando a pesquisa não encontra resultados</response>
    /// <response code="500">Retorna quando algum problema inesperado acontece na chamada</response>
    /// <returns>Retorna uma lista contendo os weathers que atendam a pesquisa por filtro</returns>
    [HttpGet(Name = "summary")]
    [MapToApiVersion("2.0")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CommandResult>> GetWeathersBySummaryV2Async([FromQuery] string summary)
    {
        var weathers = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray().Where(x => x.Summary.Contains(summary));

        var commandResult = new CommandResult(weathers, true, "verifique os dados");

        return FormatApiResponse(commandResult);
    }
}

