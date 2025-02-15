using System.Security.Claims;
using LSCore.Contracts;
using LSCore.Contracts.Configurations;
using LSCore.Contracts.Exceptions;
using LSCore.Framework.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LSCore.Framework.Middlewares;

public class LSCoreApiKeyAuthorizationMiddleware(RequestDelegate next, ILogger<LSCoreApiKeyAuthorizationMiddleware> logger, LSCoreApiKeyConfiguration configuration)
{
    public async Task Invoke(HttpContext context)
    {
        // If the request is already authenticated, then we can skip the rest of the middleware
        if (context.User.Identity?.IsAuthenticated == true)
        {
            await next(context);
            return;
        }
        
        var shouldVerify = configuration.AuthorizeAll;
        if (!shouldVerify) // If we don't need to authorize all requests, check if the current endpoint requires authorization
            shouldVerify = context.GetEndpoint()?.Metadata.GetMetadata<LSCoreAuthorizeAttribute>() != null;

        if (!shouldVerify)
        {
            await next(context);
            return;
        }
            
        var apiKey = context.Request.Headers[LSCoreContractsConstants.ApiKeyCustomHeader].FirstOrDefault();
        // If no API key is provided, then the request is unauthenticated
        if (configuration.BreakOnFailedAuthorization && string.IsNullOrWhiteSpace(apiKey))
            throw new LSCoreUnauthenticatedException();
        if (!configuration.ApiKeys.Contains(apiKey!))
        {
            if (configuration.BreakOnFailedAuthorization)
                throw new LSCoreForbiddenException();
        }
        else
        {
            context.User = new ClaimsPrincipal(new ClaimsIdentity("ApiKey"));
        }
        await next(context);
    }
}