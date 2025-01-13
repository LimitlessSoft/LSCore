using LSCore.Contracts;
using LSCore.Contracts.Configurations;
using LSCore.Contracts.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LSCore.Framework.Middlewares;

public class LSCoreApiKeyAuthorizationMiddleware(RequestDelegate next, ILogger<LSCoreApiKeyAuthorizationMiddleware> logger, LSCoreApiKeyConfiguration apiKeyConfiguration)
{
    public async Task Invoke(HttpContext context)
    {
        var apiKey = context.Request.Headers[LSCoreContractsConstants.ApiKeyCustomHeader].FirstOrDefault();
        
        // If no API key is provided, then the request is unauthenticated
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new LSCoreUnauthenticatedException();
        
        // If the API key is not in the list of allowed API keys, then the request is forbidden
        if (!apiKeyConfiguration.ApiKeys.Contains(apiKey))
            throw new LSCoreForbiddenException();
        
        await next(context);
    }
}