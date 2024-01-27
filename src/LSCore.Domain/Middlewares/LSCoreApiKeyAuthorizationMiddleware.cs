using LSCore.Contracts;
using LSCore.Contracts.SettingsModels;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace LSCore.Domain.Middlewares
{
    public class LSCoreApiKeyAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LSCoreApiKeysSettings _apiKeySettings;

        public LSCoreApiKeyAuthorizationMiddleware(RequestDelegate next, LSCoreApiKeysSettings apiKeysSettings)
        {
            _next = next;
            _apiKeySettings = apiKeysSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.User.Identity != null && !context.User.Identity.IsAuthenticated)
            {
                var requestApiKey = context.Request.Headers[LSCoreContractsConstants.ApiKeyCustomHeader].FirstOrDefault();
                if(string.IsNullOrWhiteSpace(requestApiKey) || !_apiKeySettings.ApiKeys.Contains(requestApiKey))
                {
                    context.Response.StatusCode = 403;
                    return;
                }
            }
            await _next(context);
        }
    }
}
