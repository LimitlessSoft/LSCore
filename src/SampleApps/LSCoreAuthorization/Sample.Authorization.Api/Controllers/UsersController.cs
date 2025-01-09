using LSCore.Framework.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Authorization.Contracts.Interfaces.IManagers;
using Sample.Authorization.Contracts.Requests.Users;

namespace Sample.Authorization.Api.Controllers;

public class UsersController(IUserManager userManager) : ControllerBase
{
    [HttpPost]
    [Route("/set-password")]
    public IActionResult SetPassword([FromBody] UsersSetPasswordRequest request)
    {
        userManager.SetPassword(request);
        return Ok();
    }
    
    [HttpPost]
    [Route("/authorize")]
    public IActionResult AuthorizeUser([FromBody] UsersAuthorizeRequest request) =>
        Ok(userManager.Authorize(request.Username, request.Password));
    
    [HttpGet]
    [Route("/me")]
    public IActionResult GetMe() =>
        Ok(userManager.GetMe());
    
    [HttpGet]
    [LSCoreAuthorize]
    [Route("/authenticated-only")]
    public IActionResult GetAuthenticatedOnly() =>
        Ok();
}