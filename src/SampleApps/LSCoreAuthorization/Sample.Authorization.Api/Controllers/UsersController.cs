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
        userManager.SetPassword(request.Password);
        return Ok();
    }
    
    [HttpPost]
    [Route("/authorize")]
    public IActionResult AuthorizeUser([FromBody] UsersAuthorizeRequest request) =>
        Ok(userManager.Authorize(request.Username, request.Password));
}