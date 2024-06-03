using System.Net;
using LSCore.Contracts.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
            HandleException(context, ex);
        }
    }

    private void HandleException(HttpContext context, Exception exception)
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
                context.Response.WriteAsync(exception.Message);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
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