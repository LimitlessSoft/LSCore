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
/// <typeparam name="TPermissionEnum"></typeparam>
public class LSCoreAuthorizePermissionAttribute<TPermissionEnum> (params TPermissionEnum[] permissions) : LSCoreAuthorizeAttribute where TPermissionEnum : Enum
{
    public TPermissionEnum[] Permissions { get; } = permissions;
}

/// <summary>
/// Used to authorize a request based on a role.
/// If user has any of the roles, then the request is authorized.
/// </summary>
/// <param name="roles"></param>
/// <typeparam name="TRoleEnum"></typeparam>
public class LSCoreAuthorizeRoleAttribute<TRoleEnum> (params TRoleEnum[] roles) : LSCoreAuthorizeAttribute where TRoleEnum : Enum
{
    public new TRoleEnum[] Roles { get; } = roles;
}