namespace LSCore.Contracts.Configurations;

public abstract class LSCoreAuthorizationConfigurationBase
{
    /// <summary>
    /// If set to true, all requests will be authorized by default, not requiring LSCoreAuthorizeAttribute.
    /// Still you can set [AllowAnonymous] attribute to skip the authorization.
    /// </summary>
    public bool AuthorizeAll { get; set; } = false;
    /// <summary>
    /// Used to break the request pipeline if the authorization fails.
    /// If set to false and the authorization fails, the request will continue
    /// to the next middleware which should handle the failed authorization.
    /// This is useful for scenarios where you have multiple authorization middlewares
    /// So this one can succeed and the next one can check if the user is authorized and just pass him through
    /// or fail the request.
    /// Example is APIKeyAuthorization > JwtAuthorization, where APIs one is set to false, so if it fails, JwtAuthorization can still pass the user through.
    /// Otherwise, if it succeeds, JwtAuthorization checks if the user is already authorized and passes him without additional checks.
    /// </summary>
    public bool BreakOnFailedAuthorization { get; set; } = true;
}