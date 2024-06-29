using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using LSCore.Contracts;

namespace LSCore.Framework.Middlewares;

public class LSCoreUseContextUser(RequestDelegate next, ILogger<LSCoreUseContextUser> logger)
{
    public async Task Invoke(HttpContext context)
    {
        
        var currentUser = context.RequestServices.GetService<LSCoreContextUser>();

        if (context.User.Identity?.IsAuthenticated == true)
        {
            currentUser!.Id = int.Parse(context.User.FindFirstValue(LSCoreContractsConstants.ClaimNames.CustomUserId)!);
        }

        await next(context);
    }
}