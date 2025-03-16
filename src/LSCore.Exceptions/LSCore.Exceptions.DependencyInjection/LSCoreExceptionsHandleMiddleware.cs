using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LSCore.Exceptions.DependencyInjection;

public class LSCoreExceptionsHandleMiddleware(
	RequestDelegate next,
	ILogger<LSCoreExceptionsHandleMiddleware> logger
)
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
