using Microsoft.AspNetCore.Authorization;

namespace LSCore.Framework
{
    public class LSCoreAuthorizationAttribute : AuthorizeAttribute
    {
        public LSCoreAuthorizationAttribute() { }
        public LSCoreAuthorizationAttribute(params object[] roles)
        {
            if (roles.Any(x => x.GetType().BaseType != typeof(Enum)))
                throw new ArrayTypeMismatchException($"Authorization objects must be of type {typeof(Enum)}");

            base.Roles = string.Join(',', roles.Select(x => x.ToString()));
        }
    }
}
