using System.Security.Claims;
using LSCore.Auth.Contracts;
using LSCore.Auth.Key.Contracts;
using LSCore.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LSCore.Auth.Key.DependencyInjection;

public class LSCoreAuthKeyMiddleware(
	RequestDelegate next,
	ILogger<LSCoreAuthKeyMiddleware> logger,
	ILSCoreAuthKeyProvider authKeyProvider,
	LSCoreAuthKeyConfiguration configuration
)
{
	public async Task Invoke(HttpContext context)
	{
		// If the request is already authenticated, then we can skip the rest of the middleware
		if (context.User.Identity?.IsAuthenticated == true)
		{
			await next(context);
			return;
		}

		var shouldVerify = configuration.AuthAll;
		if (!shouldVerify) // If we don't need to authorize all requests, check if the current endpoint requires authorization
			shouldVerify = context.GetEndpoint()?.Metadata.GetMetadata<LSCoreAuthAttribute>() != null;

		if (!shouldVerify)
		{
			await next(context);
			return;
		}

		var apiKey = context.Request.Headers[LSCoreAuthKeyHeaders.KeyCustomHeader].FirstOrDefault();
		// If no API key is provided, then the request is unauthenticated
		if (configuration.BreakOnFailedAuth && string.IsNullOrWhiteSpace(apiKey))
			throw new LSCoreUnauthenticatedException();

		// Left for backward compatibility
		if (configuration.ValidKeys != null)
		{
			if (!configuration.ValidKeys.Contains(apiKey!))
			{
				if (configuration.BreakOnFailedAuth)
					throw new LSCoreUnauthenticatedException();
			}
			else
			{
				context.User = new ClaimsPrincipal(new ClaimsIdentity("ApiKey"));
			}
		}
		else if (!authKeyProvider.IsValidKey(apiKey!))
		{
			if (configuration.BreakOnFailedAuth)
				throw new LSCoreUnauthenticatedException();
		}
		else
		{
			context.User = new ClaimsPrincipal(new ClaimsIdentity("ApiKey"));
		}
		await next(context);
	}
}
