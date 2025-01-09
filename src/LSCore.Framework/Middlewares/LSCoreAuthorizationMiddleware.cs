using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using LSCore.Contracts;

namespace LSCore.Framework.Middlewares;

public class LSCoreAuthorizationMiddleware(RequestDelegate next, ILogger<LSCoreAuthorizationMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        var currentUser = context.RequestServices.GetService<LSCoreContextUser>();

        if (context.User.Identity?.IsAuthenticated == true)
        {
            var jwtIdentifier = context.User.FindFirstValue(LSCoreContractsConstants.ClaimNames.CustomIdentifier);
            if (!string.IsNullOrWhiteSpace(jwtIdentifier))
                currentUser!.Id = int.Parse(jwtIdentifier);
        }

        await next(context);
    }
}