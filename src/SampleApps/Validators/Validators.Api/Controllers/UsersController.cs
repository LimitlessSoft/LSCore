using Microsoft.AspNetCore.Mvc;
using Validators.Contracts;

namespace Validators.Api.Controllers;

[ApiController]
public class UsersController (IUserManager userManager)
    : ControllerBase
{
    [HttpGet]
    [Route("/users")]
    public IActionResult GetUsers([FromQuery] GetUsersRequest request) =>
        Ok(userManager.GetUsers(request));
}