using Microsoft.AspNetCore.Authorization;

namespace LSCore.Framework.Attributes;

/// <summary>
/// Used to authorize a request based on authentication.
/// If user is authenticated, then the request is authorized.
/// </summary>
public class LSCoreAuthorizeAttribute : AuthorizeAttribute;

/// <summary>
/// Used to authorize a request based on a permission.
/// If user has any of the permissions, then the request is authorized.
/// </summary>
/// <param name="permissions"></param>
/// <typeparam name="T"></typeparam>
public class LSCoreAuthorizePermissionAttribute<T> (params T[] permissions) : LSCoreAuthorizeAttribute where T : Enum
{
    public T[] Permissions { get; } = permissions;
}

/// <summary>
/// Used to authorize a request based on a role.
/// If user has any of the roles, then the request is authorized.
/// </summary>
/// <param name="roles"></param>
/// <typeparam name="T"></typeparam>
public class LSCoreAuthorizeRoleAttribute<T> (params T[] roles) : LSCoreAuthorizeAttribute where T : Enum
{
    public new T[] Roles { get; } = roles;
}