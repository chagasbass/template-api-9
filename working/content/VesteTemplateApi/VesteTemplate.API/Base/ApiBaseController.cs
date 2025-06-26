namespace VesteTemplate.Api.Bases
{
    /// <summary>
    /// Controller Base da aplicação
    /// </summary>
    [ApiController]
    public abstract class ApiBaseController(ILogServices logServices, INotificationServices notificationServices) : ControllerBase
    {
        internal ActionResult FormatApiResponse(CommandResult commandResult, string? defaultEndpointRoute = null)
        {
            var statusCodeOperation = notificationServices.StatusCode;

            ICommandResult result = default;

            if (notificationServices.HasNotifications())
            {
                result = CreateErrorResponse(statusCodeOperation.Id, commandResult);

                notificationServices.ClearNotifications();
            }

            switch (statusCodeOperation)
            {
                case var _ when statusCodeOperation == StatusCodeOperation.BadRequest:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.BadRequest);
                    return BadRequest(result);
                case var _ when statusCodeOperation == StatusCodeOperation.NotFound:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.NotFound);
                    return NotFound(commandResult);
                case var _ when statusCodeOperation == StatusCodeOperation.Unauthorized:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.Unauthorized);
                    return Unauthorized(commandResult);
                case var _ when statusCodeOperation == StatusCodeOperation.BusinessError:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.UnprocessableEntity);
                    return UnprocessableEntity(commandResult);
                case var _ when statusCodeOperation == StatusCodeOperation.Created:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.Created);
                    return Created(defaultEndpointRoute, commandResult);
                case var _ when statusCodeOperation == StatusCodeOperation.NoContent:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.NoContent);
                    return NoContent();
                case var _ when statusCodeOperation == StatusCodeOperation.OK:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.OK);
                    return Ok(commandResult);
                case var _ when statusCodeOperation == StatusCodeOperation.Accepted:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.Accepted);
                    return Ok(commandResult);
                default:
                    GenerateLogResponse(commandResult, (int)HttpStatusCode.OK);
                    return Ok(commandResult);
            }
        }

        private void GenerateLogResponse(CommandResult commandResult, int statusCode)
        {
            logServices.LogData.AddResponseStatusCode(statusCode)
                                .AddResponseBody(commandResult);

            logServices.WriteLog();
            notificationServices.ClearNotifications();
        }

        private ICommandResult CreateErrorResponse(int statusCode, CommandResult commandResult)
        {
            var notifications = notificationServices.GetNotifications();

            var defaultTitle = "Verifique o retorno do request.";

            var problemDetails = new ApiProblemDetails(notifications.ToList(), commandResult?.Message, statusCode, defaultTitle);

            if (commandResult is null)
            {
                commandResult = new CommandResult(problemDetails);
            }
            else
            {
                commandResult.Data = problemDetails;
            }

            return commandResult;
        }

    }
}
