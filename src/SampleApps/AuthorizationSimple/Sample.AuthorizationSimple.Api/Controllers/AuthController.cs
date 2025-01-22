using LSCore.Domain.Managers;
using Microsoft.AspNetCore.Mvc;
using Sample.AuthorizationSimple.Api.Requests;

namespace Sample.AuthorizationSimple.Api.Controllers;

public class AuthController(LSCoreAuthorizeManager authorizeManager) : ControllerBase
{
    // Use out the box Authorize method to authenticate and authorize user or override it inside your manager (AuthManager) and use it here
    [HttpPost]
    [Route("/auth")]
    public IActionResult Auth([FromBody] AuthRequest request) =>
        Ok(authorizeManager.Authorize(request.Username, request.Password));
    
    // Use out the box Refresh method to refresh token or override it inside your manager (AuthManager) and use it here
    [HttpPost]
    [Route("/refresh")]
    public IActionResult Refresh([FromBody] RefreshRequest request) =>
        Ok(authorizeManager.Refresh(request.RefreshToken));
}