using InoveFk.Core.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace InoveFk.Core.Exceptions;

public class CustomExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CustomExceptionHandler(RequestDelegate next, ILoggerFactory logger)
    {
        _next = next;
        _logger = logger.CreateLogger("Error Handler");
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(httpContext, ex);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var status = HttpStatusCode.InternalServerError.ToString();
        var errorResponse = new ErrorResponse(
            key: "Exception", 
            message: exception.Message, 
            statusCode: status);

        _logger.LogError($"StatusCode: {status}");
        _logger.LogError($"Error: {exception.Message}");
        _logger.LogError($"Stack: {exception.StackTrace}");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
    }
}
