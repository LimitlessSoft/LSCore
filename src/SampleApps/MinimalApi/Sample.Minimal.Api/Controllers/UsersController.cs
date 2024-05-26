using LSCore.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using Sample.Minimal.Contracts.IManagers;

namespace Sample.Minimal.Api.Controllers;

[ApiController]
public class UsersController (IUserManager userManager)
    : ControllerBase
{
    [HttpGet]
    [Route("/users")]
    public IActionResult GetUsers() =>
        Ok(userManager.GetMultiple());
    
    [HttpGet]
    [Route("/users/{Id}")]
    public IActionResult GetUser([FromRoute] LSCoreIdRequest request) =>
        Ok(userManager.GetSingular(request));
}