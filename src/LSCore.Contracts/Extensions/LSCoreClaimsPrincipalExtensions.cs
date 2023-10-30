using System.Security.Claims;

namespace LSCore.Contracts.Extensions
{
    public static class LSCoreClaimsPrincipalExtensions
    {
        public static string? GetName(this ClaimsPrincipal claimPrincipal)
        {
            return claimPrincipal.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        }
    }
}
