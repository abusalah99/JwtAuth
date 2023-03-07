using Microsoft.AspNetCore.Http;
using System.Net;

namespace jwtauth;
public class GlobalRequestHandlerMiddelware : IMiddleware
{
    private readonly IResponse<string> _response;
    private readonly ILogger<GlobalRequestHandlerMiddelware> _logger;

    public GlobalRequestHandlerMiddelware(ILogger<GlobalRequestHandlerMiddelware> logger)
    {
        _response = new ErrorResponse();
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);

            ResponseResult<string> result = _response.CreateResponse(exception.Message);

            await HandleExceptionAsync(context, result);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, ResponseResult<string> result)
    {
        context.Response.ContentType = "application/json";

        string response = JsonSerializer.Serialize(result);

        await context.Response.WriteAsync(response);
    }
}
