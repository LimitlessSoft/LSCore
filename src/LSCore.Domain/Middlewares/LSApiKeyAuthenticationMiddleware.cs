using Microsoft.AspNetCore.Http;
using System.Net;

namespace LSCore.Domain.Middlewares
{
    public class LSApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public LSApiKeyAuthenticationMiddleware(RequestDelegate next, string apiKey)
        {
            _next = next;
            _apiKey = apiKey;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.User.Identity == null)
            {
                var requestApiKey = context.Request.Headers["Api-Key"].FirstOrDefault();

                if(requestApiKey == null || !requestApiKey.Equals(_apiKey))
                {
                    context.Response.StatusCode = 403;
                    return;
                }
            }
            await _next(context);
        }
    }
}
