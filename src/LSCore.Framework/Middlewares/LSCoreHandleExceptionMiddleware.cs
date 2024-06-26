using LSCore.Contracts.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace LSCore.Framework.Middlewares;

public class LSCoreHandleExceptionMiddleware(RequestDelegate next, ILogger<LSCoreHandleExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case LSCoreForbiddenException:
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                break;
            
            case LSCoreUnauthenticatedException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            
            case LSCoreBadRequestException:
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(exception.Message);
                break;
            
            case LSCoreNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            
            default:
                logger.LogError(exception, exception.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }
    }
}